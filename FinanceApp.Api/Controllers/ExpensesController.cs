using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using FinanceApp.API.Services;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return NotFound();

            return expense;
        }

        [HttpGet]
        [Route("dto")]
        public async Task<DTO> GetExpenseDto()
        {
            var dto = new DTO();
            var expenseDtos = new List<ExpenseDTO>();

            try
            {
                var accounts = await _context.Accounts.ToListAsync();
                var expenses = await _context.Expenses.ToListAsync();
                var unpaidExpenses = await expenses.Where(e => !e.Paid).ToListAsync();
                var payDeductionDict = CalculationsService.GetPayDeductionDict(accounts, expenses, "expenses");

                foreach (var expense in unpaidExpenses)
                {
                    var expenseDto = new ExpenseDTO();

                    decimal payDeduction = payDeductionDict.FirstOrDefault(e => e.Key == expense.Id).Value;
                    expenseDto.Expense = expense;
                    expenseDto.PayDeduction = payDeduction;
                    expenseDto.PaycheckPercentage = CalculationsService.GetPaycheckPercentage(payDeductionDict, payDeduction);
                    expenseDto.RequiredSavings = CalculationsService.GetExpenseRequiredSavings(payDeductionDict, expense);

                    expenseDtos.Add(expenseDto);
                }

                dto.ExpenseDtos = expenseDtos;
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
                    bill.Category = expense.Category;
                    bill.PaymentFrequency = expense.PaymentFrequency;
                    bill.PayDeduction = expense.PayDeduction;

                    await _context.AddAsync(bill);
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(GetExpense), new {id = expense.Id}, expense);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
