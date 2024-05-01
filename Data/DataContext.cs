using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class LibraryDataContext : DbContext
    {
        public LibraryDataContext(DbContextOptions<LibraryDataContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    }
}