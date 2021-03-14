using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
using FinanceApp.Api.Models;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using FinanceApp.API.Services;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Expense>> Expenses()
        {
            return await PagedListExtensions.ToListAsync(_context.Expenses.Include(e => e.Category).Include(e => e.PaymentFrequency));
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Freqency>> GetFrequencies()
        {
            return await _context.Frequencies.Where(f => f.IsActive).ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<AmortizedExpenses>> AmortizedExpenses()
        {
            var amortizedExpenses = new List<AmortizedExpenses>();
            var today = DateTime.Today;

            // Get all upcoming expenses and all unpaid past expenses 
            var expenses = await _context.Expenses.Where(e => e.DueDate <= today && !e.Paid || e.DueDate >= today).Include(e => e.PaymentFrequency).ToListAsync();
            var accounts = await _context.Accounts.ToListAsync();
            var income = await _context.Incomes.Include(i => i.PaymentFrequency).FirstOrDefaultAsync();

            var payDeductionsDict = CalculationsService.GetPayDeductionDict(accounts, expenses, "");

            foreach (var expense in expenses)
            {
                var amortizedExpense = new AmortizedExpenses();
                amortizedExpense.Name = expense.Name;
                amortizedExpense.Due = expense.DueDate;
                amortizedExpense.Amount = expense.AmountDue;
                amortizedExpense.PaydaysUntilDue = CalculationsService.GetPaydaysUntilDue(expense, income);
                amortizedExpense.RequiredSavings = CalculationsService.GetExpenseRequiredSavings(payDeductionsDict, expense, income) ?? 0.00m;

                amortizedExpenses.Add(amortizedExpense);
            }


            return amortizedExpenses;
        }

        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Expense>> Expense(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return NotFound();

            return expense;
        }

        [HttpGet]
        public async Task<ActionResult<DTO>> DTO()
        {
            var dto = new DTO();
            var expenseDtos = new List<ExpenseDTO>();
            var today = DateTime.Today;

            try
            {
                var accounts = await PagedListExtensions.ToListAsync(_context.Accounts);
                var expenses = await PagedListExtensions.ToListAsync(_context.Expenses);
                var income = _context.Incomes.FirstOrDefault();
                var unpaidExpenses = await expenses.Where(e => !e.Paid).ToListAsync();
                var payDeductionDict = CalculationsService.GetPayDeductionDict(accounts, expenses, "expenses");


                foreach (var expense in unpaidExpenses)
                {
                    var expenseDto = new ExpenseDTO();

                    decimal payDeduction = payDeductionDict.FirstOrDefault(e => e.Key == expense.Id).Value;
                    expenseDto.Expense = expense;
                    expenseDto.PayDeduction = payDeduction;
                    expenseDto.PaycheckPercentage = CalculationsService.GetPaycheckPercentage(payDeductionDict, payDeduction);
                    expenseDto.RequiredSavings = CalculationsService.GetExpenseRequiredSavings(payDeductionDict, expense, income);

                    expenseDtos.Add(expenseDto);
                }

                var expenseTransactionsThisMonth = await PagedListExtensions.ToListAsync(_context.Transactions.Where(t =>
                        t.Date.Year == today.Year && t.Date.Month == today.Month &&
                        t.Type == TransactionTypesEnum.Expense));

                dto.CostOfExpensesThisMonth = expenseTransactionsThisMonth.Sum(t => t.Amount);

                dto.ExpenseDtos = expenseDtos;

                List<Expense> expensesBeforeNextPayDay = income == null ? null : await PagedListExtensions.ToListAsync(_context.Expenses
                        .Where(
                            e => e.DueDate <= income.NextPayday
                         && e.DueDate >= CalculationsService.GetLastPayday(income)
                         || e.DueDate <= income.NextPayday && e.Paid == false));
                dto.ExpensesBeforeNextPayDay = expensesBeforeNextPayDay;
                dto.SumOfExpensesDueThisMonth = expensesBeforeNextPayDay?.Sum(e => e.AmountDue);
                var costPerPaycheck = payDeductionDict.Sum(e => e.Value);
                dto.CostOfExpensesPerPayPeriod = costPerPaycheck;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return dto;
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense([FromBody] Expense expense)
        {
            try
            {
                expense.Category = await _context.Categories.FindAsync(expense.CategoryId);
                expense.PaymentFrequency = await _context.Frequencies.FindAsync(expense.PaymentFrequencyId);
                
                // Add account if one doesn't already exist for the expense category
                var account = _context.Accounts.FirstOrDefault(a => a.Name == expense.Category.Name);

                if (account == null)
                {
                    account = new Account();
                    account.Name = expense.Category.Name;

                    await _context.Accounts.AddAsync(account);
                    await _context.SaveChangesAsync();
                }

                expense.AccountId = account.Id;
                await _context.AddAsync(expense);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(Expense), new { id = expense.Id }, expense);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Expense>> EditExpense([FromBody] Expense expense)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest($"Expense is invalid");

                var dbExpense = await _context.Expenses.FindAsync(expense.Id);

                if (dbExpense == null) return NoContent();

                dbExpense.Name = expense.Name;
                dbExpense.AmountDue = expense.AmountDue;
                dbExpense.DueDate = expense.DueDate.ToUniversalTime();
                dbExpense.PaymentFrequency = expense.PaymentFrequency;
                dbExpense.CategoryId = expense.CategoryId;

                _context.Entry(dbExpense).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(expense);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PayExpense(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var expense = await _context.Expenses.SingleOrDefaultAsync(e => e.Id == id);

                expense.Paid = true;
                expense.DatePaid = DateTime.UtcNow;
                expense.Account = await _context.Accounts.FindAsync(expense.AccountId);

                await _context.SaveChangesAsync();

                // 
                if (expense.IsBill && !_context.Expenses.Any(e => e.Name == expense.Name && e.DueDate > DateTime.Today))
                {
                    var newExpense = new Expense();
                    newExpense.Name = expense.Name;
                    newExpense.AmountDue = expense.AmountDue;
                    newExpense.AccountId = expense.AccountId;
                    newExpense.Account = await _context.Accounts.FindAsync(expense.AccountId);
                    newExpense.DueDate = CalculationsService.GetNextFrequencyDate(expense.DueDate, expense.PaymentFrequency).ToUniversalTime();
                    newExpense.IsBill = true;
                    newExpense.PaymentFrequency = expense.PaymentFrequency;
                    newExpense.CategoryId = expense.CategoryId;
                    newExpense.PayDeduction = expense.PayDeduction;

                    await _context.Expenses.AddAsync(newExpense);
                    await _context.SaveChangesAsync();
                }

                //TODO: Update all database times to UTC
                var transaction = new Transaction();
                transaction.Payee = expense.Name;
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = expense.AmountDue;
                transaction.CategoryId = expense.CategoryId;
                transaction.Type = TransactionTypesEnum.Expense;
                transaction.UserId = expense.UserId;

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();


                return AcceptedAtRoute(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(long id)
        {
            try
            {
                var expense = await _context.Expenses.FindAsync(id);

                if (expense == null) return NotFound(id);

                _context.Remove(expense);
                await _context.SaveChangesAsync();

                return StatusCode(204, expense);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
