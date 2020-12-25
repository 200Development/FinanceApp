using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceApp.Api.Entities;
using FinanceApp.Api.Enums;
using FinanceApp.API.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public BillsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/bills
        [HttpGet]
        public async Task<IEnumerable<Bill>> GetBills(
            int pageIndex = 0,
            int pageSize = 10,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null)
        {
            return await _context.Bills.ToListAsync();
        }

        // GET: api/bills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(long id)
        {
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null)
                return NotFound();

            return bill;
        }

        // POST: api/bills
        [HttpPost]
        public async Task<ActionResult<Bill>> AddBill([FromBody] Bill bill)
        {
            try
            {
                //bill.UserId = _userId;
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBill), new {id = bill.Id}, bill);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        private Bill MapToBill(BillDTO dto)
        {
            var bill = new Bill();

            bill.Name = dto.Name;
            bill.AmountDue = dto.AmountDue;
            bill.DueDate = dto.DueDate;
            bill.PaymentFrequency = dto.PaymentFrequency;
            bill.Category = dto.Category;


            return bill;
        }
    }
}
