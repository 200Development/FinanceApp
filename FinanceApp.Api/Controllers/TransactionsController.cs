using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.TeleTrust;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Transaction>> Transactions()
        {
            return await PagedListExtensions.ToListAsync(_context.Transactions.Include(t => t.Category));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return NotFound();

            return transaction;
        }
        
        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction([FromBody] Transaction transaction)
        {
            try
            {
                transaction.Category = await _context.Categories.FindAsync(transaction.CategoryId);
                transaction.Date = transaction.Date.ToUniversalTime();

                // Save new transaction
                await _context.AddAsync(transaction);
                await _context.SaveChangesAsync();

                // Create new account for transaction category if one doesn't already exist
                var account = _context.Accounts.FirstOrDefault(a => a.Name == transaction.Category.Name);
                if (account == null)
                {
                    account = new Account();
                    account.Name = transaction.Category.Name;
                    if (transaction.Type == TransactionTypesEnum.Expense)
                        account.Balance -= transaction.Amount;
                    else
                        account.Balance += transaction.Amount;

                    await _context.AddAsync(account);
                }
                else
                {
                    if (transaction.Type == TransactionTypesEnum.Expense)
                        account.Balance -= transaction.Amount;
                    else
                        account.Balance += transaction.Amount;
                }
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetTransaction), new {id = transaction.Id}, transaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Transaction>> EditTransaction([FromBody] Transaction transaction)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest($"Transaction is invalid");

                var dbTransaction = await _context.Transactions.FindAsync(transaction.Id);

                if (dbTransaction == null) return NoContent();

                dbTransaction.Payee = transaction.Payee;
                dbTransaction.Amount = transaction.Amount;
                dbTransaction.Date = transaction.Date.ToUniversalTime();
                dbTransaction.CategoryId = transaction.CategoryId;

                _context.Entry(dbTransaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(transaction);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(long id)
        {
            try
            {
                var transaction = await _context.Transactions.FindAsync(id);

                if (transaction == null) return NotFound(id);

                _context.Remove(transaction);
                await _context.SaveChangesAsync();

                return StatusCode(204, transaction);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
