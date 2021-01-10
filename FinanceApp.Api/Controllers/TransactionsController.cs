using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public TransactionsController(ApplicationDbContext _context)
        {
            this._context = _context;
        }


        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
                return NotFound();

            return transaction;
        }

        [HttpGet]
        [Route("dto")]
        public async Task<ActionResult<DTO>> GetTransactionDto()
        {
            var dto = new DTO();
            var transactionDtos = new List<TransactionDTO>();

            try
            {
                var transactions = await _context.Transactions.ToListAsync();

                foreach (var transaction in transactions)
                {
                    var transactionDto = new TransactionDTO();
                    transactionDto.Transaction = transaction;

                    transactionDtos.Add(transactionDto);
                }

                dto.TransactionDtos = transactionDtos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }


            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction([FromBody] Transaction transaction)
        {
            try
            {
                await _context.AddAsync(transaction);
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
