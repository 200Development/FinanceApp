
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dto")]
        public async Task<ActionResult<DTO>> GetDashboardDto()
        {
            try
            {
                var dto = new DTO();
                var dashboardDto = new DashboardDTO();

                var incomes = await _context.Incomes.ToListAsync();
                var income = incomes.Count > 1 ? incomes.First() : null;

                dashboardDto.Accounts = await _context.Accounts.ToListAsync();

                dashboardDto.ExpensesDueThisMonth = await PagedListExtensions.ToListAsync(_context.Expenses
                    .Where(e => e.DueDate.Year == DateTime.Now.Year && e.DueDate.Month == DateTime.Now.Month));

                dashboardDto.ExpensesDueBeforeNextPayday = await PagedListExtensions.ToListAsync(_context.Expenses
                    .Where(e => e.DueDate < new DateTime(2021, 1, 22, new GregorianCalendar())));

                if (income != null)
                {
                    dashboardDto.MonthlyIncome = income.GetMonthlyIncome();
                    dashboardDto.OutstandingMonthlyIncome = income.GetOutstandingMonthlyIncome();
                }

                if (_context.Accounts.Any())
                    dashboardDto.DisposableCash = await GetDisposableCash();

                dto.DashboardDto = dashboardDto;

                return StatusCode(200, dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<decimal> GetDisposableCash()
        {
            return await _context.Accounts.SumAsync(a => a.BalanceSurplus);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Income>> GetIncome(long id)
        {
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
                return NotFound();

            return StatusCode(200, income);
        }


        // POST: api/dashboard
        [HttpPost]
        public async Task<ActionResult<Income>> AddIncome([FromBody] Income income)
        {
            try
            {
                await _context.Incomes.AddAsync(income);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetIncome), new { id = income.Id }, income);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
