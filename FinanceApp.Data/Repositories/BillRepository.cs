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
    public class BillRepository : BaseRepository<Bill>
    {
        private readonly string _userId;


        public BillRepository(ApplicationDbContext context, string userName) : base(context)
        {
            _userId = GetUserId(userName);
        }


        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            return await FindByCondition(bill => bill.UserId == _userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetBillsByCondition(Expression<Func<Bill, bool>> expression)
        {
            return await FindByCondition(expression)
                .Where(bill => bill.UserId == _userId)
                .ToListAsync();
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await FindByCondition(bill => bill.Id.Equals(id) && bill.UserId == _userId)
                .FirstOrDefaultAsync();
        }

        public bool CreateBill(Bill bill)
        {
            try
            {
                bill.UserId = _userId;
                Create(bill);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
