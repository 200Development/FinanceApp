using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Enums;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
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
    }
}
