using System;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MetricsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/accounts/dto
        [HttpGet]
        [Route("dto")]
        public async Task<ActionResult<DTO>> GetMetricDto()
        {
            var dto = new DTO();
            var expenses = await _context.Expenses.ToListAsync();
            var accounts = await _context.Accounts.ToListAsync();
            var incomes = await _context.Incomes.ToListAsync();
            var requiredSavingsForExpenses = 0.00m;
            var totalExpensesBeforeNextPayday = 0.00m;
            var income = incomes.FirstOrDefault();
            var today = DateTime.Today;

            // Next Pay Day
            dto.NextPayDay = income.NextPayday;

            // Expected Monthly Income
            var expectedMonthlyIncome = CalculationsService.GetCurrentMonthIncome(incomes);
            dto.ExpectedMonthlyIncome = expectedMonthlyIncome;

            // Expected Monthly Expenses
            var expectedMonthlyExpenses = CalculationsService.GetExpectedMonthlyExpenses(expenses);
            dto.ExpectedMonthlyExpenses = expectedMonthlyExpenses;

            // Expected Monthly Savings
            dto.ExpectedMonthlySavings = expectedMonthlyIncome - expectedMonthlyExpenses;


            var incomeThisMonth = 3456.43m;
            dto.IncomeThisMonth = incomeThisMonth;

            var expensesThisMonth = 2343.56m;
            dto.ExpensesThisMonth = expensesThisMonth;

            dto.SavingsThisMonth = incomeThisMonth - expensesThisMonth;

            dto.IncomePercentage = (incomeThisMonth / expectedMonthlyIncome) * 100;
            dto.ExpensePercentage = (expensesThisMonth / expectedMonthlyExpenses) * 100;
            dto.SavingsPercentage = (incomeThisMonth + expensesThisMonth) / (expectedMonthlyIncome + expectedMonthlyExpenses) * 100;

            // Cost of Discretionary Expenses This Month
            var discretionaryExpenses = await expenses.Where(e => e.DueDate.Year == today.Year && e.DueDate.Month == today.Month && e.IsBill == false).ToListAsync();
            dto.CostOfDiscretionaryExpensesThisMonth = discretionaryExpenses.Sum(e => e.AmountDue);

            var mandatoryExpenses = await expenses
                .Where(e => e.DueDate.Year == today.Year && e.DueDate.Month == today.Month && e.IsBill).ToListAsync();
            dto.CostOfMandatoryExpensesThisMonth = mandatoryExpenses.Sum(e => e.AmountDue);

            var payDeductionDict = CalculationsService.GetPayDeductionDict(accounts, expenses, "expenses");


            foreach (var expense in expenses.Where(e => e.Paid == false))
            {
                if (payDeductionDict != null)
                    requiredSavingsForExpenses += CalculationsService.GetExpenseRequiredSavings(payDeductionDict, expense, income).Value;
            }

            dto.RequiredSavings = requiredSavingsForExpenses;

            var expensesDueBeforeNextPayDay = await expenses
                .Where(e => e.DueDate.Date <= income?.NextPayday.Date).ToListAsync();
            dto.ExpensesDueBeforeNextPayDay = expensesDueBeforeNextPayDay;

            totalExpensesBeforeNextPayday = expensesDueBeforeNextPayDay.Sum(e => e.AmountDue);
            dto.TotalExpensesDueBeforeNextPayDay = totalExpensesBeforeNextPayday;

            dto.ExpensesDueThisMonth = await _context.Expenses
                .Where(e => e.DueDate.Year == today.Year && e.DueDate.Month == today.Month).ToListAsync();


            var cashBalance = accounts.Sum(a => a.Balance);
            dto.CashBalance = cashBalance;

            dto.AccountingBalance = cashBalance - requiredSavingsForExpenses;
            dto.Month = GetMonthName(today.Month);
            dto.Year = today.Year;

            return dto;
        }

        private string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    throw new ArgumentNullException();
            }
        }
    }
}
