using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Data.Base;
using FinanceApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await FindAll()
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await FindByCondition(account => account.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public void UpdateAccount(Account account)
        {
            Update(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }
    }
}
