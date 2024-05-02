using Domain.Entities;

namespace Data.Contracts
{
    public interface IBorrowedBookRepository : IRepository<BorrowedBook>
    {
        public Task<BorrowedBook> GetBorrowedBookByKey(int key);

        public Task<IEnumerable<BorrowedBook>> GetBorrowedBooks();
    }
}