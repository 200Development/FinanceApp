using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Repositories
{
    /// <summary>
    /// Handles all calls to the Accounts table in the database.  Exposes the signed-in User to create and follow the User - Entity relationship. 
    /// </summary>
    public class AccountRepository : BaseRepository<Account>
    {
        private readonly string _userId;


        public AccountRepository(ApplicationDbContext context, string userName = "") : base(context)
        {
            //_userId = GetUserId(userName);
        }


        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await FindByCondition(account => account.UserId == _userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsByCondition(Expression<Func<Account, bool>> expression)
        {
            return await FindByCondition(expression)
                .Where(account => account.UserId == _userId)
                .ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await FindByCondition(account => account.UserId == _userId && account.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetDisposableIncomeAccount()
        {
            return await FindByCondition(account => account.UserId == _userId && account.IsDisposableIncomeAccount)
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetEmergencyFundAccount()
        {
            return await FindByCondition(account => account.UserId == _userId && account.IsEmergencyFund)
                .FirstOrDefaultAsync();
        }

        public void CreateAccount(Account account)
        {
            try
            {
                account.UserId = _userId;
               
                Create(account);
                Save();
               
               
            }
            catch(Exception e)
            {
               // empty
            }
        }
    }
}
