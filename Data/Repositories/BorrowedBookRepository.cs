using Data.Contracts;
using Domain.Entities;

namespace Data.Repositories
{
    public class BorrowedBookRepository : Repository<BorrowedBook>, IBorrowedBookRepository
    {
        public BorrowedBookRepository(DataContext context) : base(context)
        {

        }

        async Task<BorrowedBook> IBorrowedBookRepository.GetBorrowedBookByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(b => b.BorrowID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<BorrowedBook>> IBorrowedBookRepository.GetBorrowedBooks()
        {
            try
            {
                return await GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}