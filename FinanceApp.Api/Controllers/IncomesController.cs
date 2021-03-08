using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceApp.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Income>> Incomes()
        {
            return await PagedListExtensions.ToListAsync(_context.Incomes);
        }

        [HttpGet]
        public async Task<ActionResult<Income>> GetIncome(long id)
        {
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
                return NotFound();

            return StatusCode(201, income);
        }

        [HttpPost]
        public async Task<ActionResult<Income>> AddIncome([FromBody] Income income)
        {
           
            await _context.Incomes.AddAsync(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIncome), new {id = income.Id}, income);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateNextPayday()
        {
            var income = await _context.Incomes.FirstOrDefaultAsync();
            income.UpdateNextPayday();

            return Ok();
        }
    }
}
