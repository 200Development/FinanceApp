using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
using FinanceApp.API.Enums;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using FinanceApp.API.Services;
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
            return await _context.Bills.Include(b => b.Account).ToListAsync();
        }

        // GET: api/bills/dto
        [HttpGet]
        [Route("dto")]
        public async Task<DTO> GetBillDto()
        {
            var dto = new DTO();
            var billDtos = new List<BillDTO>();

            try
            {
                var bills = await _context.Bills.ToListAsync();
                var accounts = await _context.Accounts.ToListAsync();
                var income = await _context.Incomes.FirstOrDefaultAsync();
                var payDeductionDict = CalculationsService.GetPayDeductionDict(accounts, bills, "bill");

                foreach (var bill in bills)
                {
                    var billDto = new BillDTO();
                    billDto.Bill = bill;
                    
                    decimal payDeduction = payDeductionDict.FirstOrDefault(b => b.Key == bill.Id).Value;
                   
                    billDto.PayDeduction = payDeduction;
                    billDto.PaycheckPercentage = CalculationsService.GetPaycheckPercentage(payDeductionDict, payDeduction);
                    billDto.RequiredSavings = CalculationsService.GetBillRequiredSavings(payDeductionDict, bill, income);

                    billDtos.Add(billDto);
                }

                dto.BillDtos = billDtos;
                var costPerPaycheck = payDeductionDict.Sum(b => b.Value);
                dto.CostOfBillsPerPayPeriod = costPerPaycheck;
                dto.MonthlyCostOfBills = costPerPaycheck * 26 / 12;

                return dto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

                var expense = new Expense();
                expense.UserId = bill.UserId;
                expense.Name = bill.Name;
                expense.AmountDue = bill.AmountDue;
                expense.DueDate = bill.DueDate;
                expense.CategoryId = bill.CategoryId;
                expense.PaymentFrequency = bill.PaymentFrequency;
                expense.PayDeduction = bill.PayDeduction;
                expense.AccountId = bill.AccountId;
                expense.DatePaid = DateTime.MinValue;
                expense.Paid = false;

                await _context.Expenses.AddAsync(expense);
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

            bill.Name = dto.Bill.Name;
            bill.AmountDue = dto.Bill.AmountDue;
            bill.DueDate = dto.Bill.DueDate;
            bill.PaymentFrequency = dto.Bill.PaymentFrequency;
            bill.Category = dto.Bill.Category;


            return bill;
        }
    }
}
