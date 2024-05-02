using Data.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Contracts
{
    /// <summary>
    /// IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        // Author
        IAuthorRepository AuthorRepository { get; }
    }
}