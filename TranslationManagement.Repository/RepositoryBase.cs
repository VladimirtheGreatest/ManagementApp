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
        public void RemoveMultiple(IEnumerable<T> entities) => _context.RemoveRange(entities);

  public void CommitIsolatedTransactionForEntity(Action methodToExecute)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
             strategy.Execute( () =>
            {
                //start a transaction with the higher isolation level
                using (var dbContextTransaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        methodToExecute();
                        // run the insert statement
                        _context.SaveChanges();
                        //commit the transaction
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        //roll back the transaction if anything went wrong so that your database isn't locked
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public async Task CommitIsolatedTransactionForEntityFuncAsync(Func<Task> methodToExecute)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                //start a transaction with the higher isolation level
                using (var dbContextTransaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        await methodToExecute();
                        // run the insert statement
                        await _context.SaveChangesAsync();
                        //commit the transaction
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        //roll back the transaction if anything went wrong so that your database isn't locked
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            });
        }

    }
}
