using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        public Task<Book> GetBookByKey(int key);

        public Task<IEnumerable<Book>> GetBooks();
    }
}
