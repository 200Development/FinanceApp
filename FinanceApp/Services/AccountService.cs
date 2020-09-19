using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Repositories;
using FinanceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace FinanceApp.Services
{
    /// <summary>
    /// Handles business logic for Accounts
    /// </summary>
    public class AccountService : BaseService
    {
        private readonly AccountRepository _accountRepository;


        public AccountService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
            : base(context, config, httpContextAccessor)
        {
            _accountRepository = new AccountRepository(context, UserName);
        }

        
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        public async Task<AccountViewModel> GetViewModelAsync(int page, string sortOrder)
        {
            try
            {

                var pageSize = Convert.ToInt32(Config["defaultPageSize"]);

                var accountVM = new AccountViewModel();
                accountVM.Accounts = (IList<Account>)await _accountRepository.GetAllAccountsAsync();

                switch (sortOrder)
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
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            try
            {
                return await _accountRepository.GetAllAccountsAsync();
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsByConditionAsync(Expression<Func<Account, bool>> expression)
        {
            try
            {
                return await _accountRepository.GetAccountsByCondition(expression);
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            try
            {
                return await _accountRepository.GetAccountByIdAsync(id);
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        public async Task<Account> GetPoolAccountAsync()
        {
            try
            {
                return await _accountRepository.GetPoolAccount();
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        public async Task<Account> GetEmergencyFundAccountAsync()
        {
            try
            {
                return await _accountRepository.GetEmergencyFundAccount();
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return null;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static decimal UpdateBalanceSurplus(Account account)
        {
            try
            {
                // If account balance is < $0.00, balance surplus = balance.
                // If there's a balance limit AND the account balance is greater than the balance limit, balance surplus = any funds over the balance limit
                // If the balance limit is > balance, balance surplus = $0.00 since there is no surplus
                // If there's no balance limit, balance surplus = any funds over the required savings
                if (account.Balance < 0.0m)
                    return account.Balance;

                if (account.BalanceLimit > 0.0m)
                    return account.Balance > account.BalanceLimit
                        ? account.Balance - account.BalanceLimit
                        : 0.0m;

                return account.Balance - account.RequiredSavings;
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                return 0.0m; ;
            }
        }

        public async Task CheckAndCreatePoolAccountAsync()
        {
            try
            {
                var accounts = await _accountRepository.GetAllAccountsAsync();
                if (accounts.Any(a => a.IsPoolAccount)) return;

                var poolAccount = new Account();
                poolAccount.Name = "Pool";
                poolAccount.Balance = 0.0m;
                poolAccount.IsPoolAccount = true;
                poolAccount.IsEmergencyFund = false;

                _accountRepository.CreateAccount(poolAccount);
                _accountRepository.Save();
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw;
            }
        }

        public async Task CheckAndCreateEmergencyFundAsync()
        {
            try
            {
                var accounts = await _accountRepository.GetAllAccountsAsync();
                if (accounts.Any(a => a.IsEmergencyFund)) return;

                var efAccount = new Account();
                efAccount.Name = "Emergency Fund";
                efAccount.Balance = 0.0m;
                efAccount.IsPoolAccount = false;
                efAccount.IsEmergencyFund = true;

                _accountRepository.CreateAccount(efAccount);
                _accountRepository.Save();
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw;
            }
        }

        public async Task CheckAndCreateAddNewAccountAsync()
        {
            try
            {
                var accounts = await _accountRepository.GetAllAccountsAsync();
                if (accounts.Any(a => a.IsAddNewAccount)) return;

                var anAccount = new Account();
                anAccount.Name = "Add New Account";
                anAccount.Balance = 0.0m;
                anAccount.IsPoolAccount = false;
                anAccount.IsEmergencyFund = false;
                anAccount.IsAddNewAccount = true;

                _accountRepository.CreateAccount(anAccount);
                _accountRepository.Save();
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e);
                throw;
            }
        }

        public bool CreateAccount(Account account, bool save = true)
        {
            try
            {
                // Call AccountRepository to assign User ID to Account
                _accountRepository.CreateAccount(account);

                if (save)
                    _accountRepository.Save();

                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return false;
            }
        }

        public void UpdateAccount(Account entity, bool save = true)
        {
            try
            {
                _accountRepository.Update(entity);
                if (save)
                    _accountRepository.Save();
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        public bool DeleteExpense(Account entity, bool save = true)

        {
            try
            {
                _accountRepository.Delete(entity);
                if (save)
                    _accountRepository.Save();

                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return false;
            }
        }

        public bool SaveAccount(bool acceptAllChangesOnSuccess = true)
        {
            try
            {
                _accountRepository.Save(acceptAllChangesOnSuccess);

                return true;
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);

                return false;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);

                return false;
            }
        }
    }
}
