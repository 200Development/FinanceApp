using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Data.Models.Entities;
using FinanceApp.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace FinanceApp.Services
{
    /// <summary>
    /// Manages all read/write to database Expense Table
    /// </summary>
    public class ExpenseService : BaseService
    {
        private readonly ExpenseRepository _expenseRepository;

        public ExpenseService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(context, config, httpContextAccessor)
        {
            _expenseRepository = new ExpenseRepository(context, UserName);
        }


        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            try
            {
                return await _expenseRepository.GetAllExpensesAsync();
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

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            try
            {
                return await _expenseRepository.GetExpenseByIdAsync(expenseId);
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

        public async Task<IEnumerable<Expense>> GetAllUnpaidExpenses()
        {
            try
            {
                return await _expenseRepository.GetAllUnpaidExpensesAsync();
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

        public async Task<IEnumerable<Expense>> GetAllExpensesForBillAsync(int billId)
        {
            try
            {
                return await _expenseRepository.GetAllExpensesForBillAsync(billId);
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

        public async Task<IEnumerable<Expense>> GetExpensesByConditionAsync(Expression<Func<Expense, bool>> expression)
        {
            try
            {
                return await _expenseRepository.GetExpensesByCondition(expression);
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

        public async Task<decimal> GetOutstandingExpensesTotal()
        {
            try
            {
                var expenses = await GetAllUnpaidExpenses();

                return expenses.Sum(b => b.Amount);
            }
            catch (SqlException e)
            {
                Logger.Instance.Error(e);
                return decimal.MinValue;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                return decimal.MinValue;
            }
        }

        public async Task<bool> SetExpenseToPaid(int expenseId, bool save = true)
        {
            try
            {
                var selectedExpense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

                selectedExpense.IsPaid = true;
                _expenseRepository.SetEntityStateToModified(selectedExpense);
                if (save)
                    _expenseRepository.Save();


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

        public async Task<bool> SetExpenseToUnpaid(int expenseId, bool save = true)
        {
            try
            {
                var selectedExpense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

                selectedExpense.IsPaid = false;
                _expenseRepository.SetEntityStateToModified(selectedExpense);
                if (save)
                    _expenseRepository.Save();


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

        public bool CreateExpense(Expense entity, bool save = true)
        {
            try
            {
                // Call ExpenseRepository to assign User ID to Expense
                _expenseRepository.CreateExpense(entity);
                if (save)
                    _expenseRepository.Save();

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

        public void UpdateExpense(Expense entity, bool save = true)
        {
            _expenseRepository.Update(entity);
            if (save)
                _expenseRepository.Save();
        }

        public bool DeleteExpense(Expense entity, bool save = true)
        {
            try
            {
                _expenseRepository.Delete(entity);
                if (save)
                    _expenseRepository.Save();

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
        
        public bool SaveExpense(bool acceptAllChangesOnSuccess = true)
        {
            try
            {
                _expenseRepository.Save(acceptAllChangesOnSuccess);

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
