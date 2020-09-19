using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceApp.Data.Base;
using FinanceApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace FinanceApp.Data.Repositories
{
    /// <summary>
    /// Handles all calls to the Transactions table in the database.  Exposes the signed-in User to create and follow the User - Entity relationship. 
    /// </summary>
    public class TransactionRepository : BaseRepository<Transaction>
    {
        private readonly string _userId;


        public TransactionRepository(ApplicationDbContext context, string userName) : base(context)
        {
            _userId = GetUserId(userName);
        }


        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await FindByCondition(transaction => transaction.UserId == _userId)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Transaction>> GetExpensesByCondition(Expression<Func<Transaction, bool>> expression)
        {
            return await FindByCondition(expression)
                .Where(transaction => transaction.UserId == _userId)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await FindByCondition(transaction => transaction.UserId == _userId && transaction.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public bool CreateTransaction(Transaction transaction)
        {
            try
            {
                transaction.UserId = _userId;
                Create(transaction);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
