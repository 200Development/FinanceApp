using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
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
            return await PagedListExtensions.ToListAsync(_context.Expenses);
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

                // Add account if one doesn't already exist for the expense category
                var account = _context.Accounts.FirstOrDefault(a => a.Name == expense.Category.ToString());

                if (account == null)
                {
                    account = new Account();
                    account.Name = expense.Category.ToString();

                    await _context.Accounts.AddAsync(account);
                    await _context.SaveChangesAsync();
                }

                expense.AccountId = account.Id;
                await _context.AddAsync(expense);
                await _context.SaveChangesAsync();

                if (expense.IsBill)
                {
                    var bill = new Bill();
                    bill.Name = expense.Name;
                    bill.AmountDue = expense.AmountDue;
                    bill.DueDate = expense.DueDate;
                    bill.AccountId = account.Id;
                    bill.CategoryId = expense.CategoryId;
                    bill.PaymentFrequency = expense.PaymentFrequency;
                    bill.PayDeduction = expense.PayDeduction;

                    await _context.AddAsync(bill);
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(Expense), new {id = expense.Id}, expense);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
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
                    newExpense.DueDate = CalculationsService.GetNextFrequencyDate(expense.DueDate, expense.PaymentFrequency);
                    newExpense.IsBill = true;
                    newExpense.PaymentFrequency = expense.PaymentFrequency;
                    newExpense.Category = expense.Category;
                    newExpense.PayDeduction = expense.PayDeduction;

                    await _context.Expenses.AddAsync(newExpense);
                    await _context.SaveChangesAsync();
                }

                //TODO: Update all database times to UTC
                var transaction = new Transaction();
                transaction.Payee = expense.Name;
                transaction.Date = DateTime.UtcNow;
                transaction.Amount = expense.AmountDue;
                transaction.Category = expense.Category;
                transaction.Type = TransactionTypesEnum.Expense;
                transaction.UserId = expense.UserId;

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                

                return AcceptedAtRoute(true);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
