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
    /// Handles all calls to the Expenses table in the database.  Exposes the signed-in User to create and follow the User - Entity relationship. 
    /// </summary>
    public class ExpenseRepository : BaseRepository<Expense>
    {
        private readonly string _userId;


        public ExpenseRepository(ApplicationDbContext context, string userName) : base(context)
        {
            _userId = GetUserId(userName);
        }


        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await FindByCondition(expense => expense.UserId == _userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetExpensesByCondition(Expression<Func<Expense, bool>> expression)
        {
            return await FindByCondition(expression)
                .Where(expense => expense.UserId == _userId)
                .ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await FindByCondition(expense => expense.UserId == _userId && expense.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllUnpaidExpensesAsync()
        {
            return await FindByCondition(expense => expense.UserId == _userId && expense.IsPaid == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesForBillAsync(int billId)
        {
            return await FindByCondition(expense => expense.UserId == _userId && expense.BillId == billId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetAllUnpaidExpensesForBillAsync(int billId)
        {
            return await FindByCondition(expense => expense.UserId == _userId && expense.BillId == billId && expense.IsPaid == false)
                .ToListAsync();
        }

        public bool CreateExpense(Expense expense)
        {
            try
            {
                expense.UserId = _userId;
                Create(expense);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
