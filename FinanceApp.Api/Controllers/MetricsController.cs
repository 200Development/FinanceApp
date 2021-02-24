using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
using FinanceApp.Api.Models;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MetricsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/metrics/dto
        [HttpGet]
        public async Task<ActionResult<DTO>> DTO()
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

            dto.IncomePercentage = Convert.ToInt32(incomeThisMonth / expectedMonthlyIncome * 100);
            dto.ExpensePercentage = Convert.ToInt32(expensesThisMonth / expectedMonthlyExpenses * 100);
            dto.SavingsPercentage = Convert.ToInt32((incomeThisMonth + expensesThisMonth) / (expectedMonthlyIncome + expectedMonthlyExpenses) * 100);

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

        // GET: api/metrics/cash-flow-graph
        [HttpGet]
        public async Task<ActionResult<List<CashFlowGraph>>> CashFlowGraph(DateTime beginDate = default, DateTime endDate = default)
        {
            var graphData = new List<CashFlowGraph>();

            if (beginDate == default)
                beginDate = DateTime.Today.AddMonths(-6);

            if (endDate == default)
                endDate = DateTime.Today;

            var initializeDate = beginDate;

            // Initialize dictionary
            while (initializeDate <= endDate)
            {
                var cashFlowGraph = new CashFlowGraph();
                var cashFlowData = new CashFlowData();

                initializeDate = initializeDate.AddMonths(1);
                var key = DateToDictionaryKey(initializeDate);

                //cashFlowData.Income = 5632.65m;
                //cashFlowData.Expenses = 3104.75m;
                //cashFlowData.CashFlow = cashFlowData.Income - cashFlowData.Expenses;

                cashFlowData.Income = decimal.Zero;
                cashFlowData.Expenses = decimal.Zero;
                cashFlowData.CashFlow = decimal.Zero;

                cashFlowGraph.Date = key;
                cashFlowGraph.DataPoints = cashFlowData;

                graphData.Add(cashFlowGraph);
            }

            var transactions = await _context.Transactions.Where(t => t.Date >= beginDate && t.Date <= endDate)
                .ToListAsync();

            foreach (var transaction in transactions)
            {
                var key = DateToDictionaryKey(transaction.Date);
                var income = transaction.Type == TransactionTypesEnum.Income ? transaction.Amount : decimal.Zero;
                var expenses = transaction.Type == TransactionTypesEnum.Expense ? (transaction.Amount * -1) : decimal.Zero;

                // Update existing month/year
                var data = graphData.FirstOrDefault(d => d.Date == key);

                if (data == null) continue;
                data.DataPoints.Income += income;
                data.DataPoints.Expenses += expenses;
            }


            return graphData;
        }

        private string DateToDictionaryKey(DateTime date)
        {
            var month = GetMonthName(date.Month);

            return $"{month}/{date.Year}";
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
