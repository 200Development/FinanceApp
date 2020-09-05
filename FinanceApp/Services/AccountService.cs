using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Repositories;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace FinanceApp.Services
{
    public class AccountService
    {
        private readonly IConfiguration _configuration;
        private readonly AccountRepository _accountRepository;


        public AccountService(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _accountRepository = new AccountRepository(context);
        }

        public async Task<AccountViewModel> GetAccountViewModel(int page, string sortParam)
        {
            var pageSize = Convert.ToInt32(_configuration["defaultPageSize"]);

            var accountVM = new AccountViewModel();
            // TODO: Research what type of collection to use, preferably avoid using cast
            accountVM.Accounts = (IList<Account>)await _accountRepository.GetAllAccountsAsync();

            switch (sortParam)
            {
                case "name_desc":
                    accountVM.Accounts.OrderByDescending(a => a.Name);
                    break;
                case "Balance":
                    accountVM.Accounts.OrderBy(a => a.Balance);
                    break;
                case "balance_desc":
                    accountVM.Accounts.OrderByDescending(a => a.Balance);
                    break;
                default:
                    accountVM.Accounts.OrderBy(a => a.Name);
                    break;
            } 
            
            accountVM.PagedAccounts = accountVM.Accounts.ToPagedList(page, pageSize);
            

            return accountVM;
        }
    }
}
