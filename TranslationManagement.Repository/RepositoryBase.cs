using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TranslationManagement.Repository.Contracts;

namespace TranslationManagement.Repository
{
    public abstract class RepositoryBase<TContext, T> : IRepositoryBase<T> where T : class where TContext : DbContext
    {
        protected TContext _context;
        public RepositoryBase(TContext context) => this._context = context;

        public async Task Add(T entity) => await _context.AddAsync(entity);

        public async Task<T> ExecuteWithExceptionHandling(Func<Task<T>> action)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw; // Rethrow the exception to propagate it
            }
        }

    }
}
