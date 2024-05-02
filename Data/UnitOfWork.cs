using Data.Contracts;
using Data.Repositories;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly DataContext context;
        private bool _disposed;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        #region Authors
        private IAuthorRepository authorRepository;
        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(context);

                return authorRepository;
            }
        }
        #endregion

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}