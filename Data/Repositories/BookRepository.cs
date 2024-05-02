using Data.Contracts;
using Domain.Entities;

namespace Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {

        }

        async Task<Book> IBookRepository.GetBookByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(b => b.BookID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<Book>> IBookRepository.GetBooks()
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