﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Api.Models.DTOs;
using FinanceApp.Api.Models.Entities;
using FinanceApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FinanceApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/accounts
        // GET: api/accounts/?pageIndex=0&pageSize=10
        // GET: api/accounts/?pageIndex=0&pageSize=10&sortColumn=name&sortOrder=asc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Accounts()
        {
            try
            {
                return await PagedListExtensions.ToListAsync(_context.Accounts);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> CashAccounts()
        {
            try
            {
                return await PagedListExtensions.ToListAsync(_context.Accounts.Where(a => a.IsCashAccount));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(long id)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(id);

                if (account == null)
                    return NotFound();

                return account;
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // POST: api/accounts/addAccount
        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount([FromBody] Account account)
        {
            try
            {
                //account.UserId = _userId;
                account.IsCashAccount = true;
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // POST: api/accounts/editAccount
        [HttpPut]
        public async Task<ActionResult<Account>> EditAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest($"Account.Name: {account.Name}, Account.Id: {account.Id} is invalid");

                var dbAccount = await _context.Accounts.FindAsync(account.Id);

                if (dbAccount == null) return BadRequest($"No account with id: {account.Id}");

                dbAccount.Name = account.Name;
                dbAccount.Balance = account.Balance;

                _context.Entry(dbAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(account);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(long id)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(id);

                if (account == null) return NotFound();

                _context.Remove(account);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
