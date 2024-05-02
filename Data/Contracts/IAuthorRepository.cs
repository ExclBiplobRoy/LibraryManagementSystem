using Domain.Entities;

namespace Data.Contracts
{
    public interface IAuthorRepository : IRepository<Author>
    {
        public Task<Author> GetAuthorByKey(int key);

        public Task<IEnumerable<Author>> GetAuthors();
    }
}