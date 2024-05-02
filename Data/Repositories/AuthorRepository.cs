using Data.Contracts;
using Domain.Entities;

namespace Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context)
        {

        }

        async Task<Author> IAuthorRepository.GetAuthorByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(b => b.AuthorID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<Author>> IAuthorRepository.GetAuthors()
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