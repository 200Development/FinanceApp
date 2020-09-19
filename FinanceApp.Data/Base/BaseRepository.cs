using System;
using System.Linq;
using System.Linq.Expressions;
using FinanceApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;


        protected BaseRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        
        // TODO: figure out best accessors to enforce using services when assigning/filtering User ID is involved
        
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public void Save(bool acceptAllChangesOnSuccess = true)
        {
            this._context.SaveChanges(acceptAllChangesOnSuccess);
        }

        public void SetEntityStateToModified(T entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
        }

        protected string GetUserId(string userName)
        {
            return this._context.Users.FirstOrDefault(user => user.UserName == userName)?.Id;
        }
    }
}