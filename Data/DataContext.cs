using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin { AdminID = 1, FirstName = "User", SurName = "Admin", Email = "admin@gmail.com", Password = "12345", Level = 5 }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorID = 1, AuthorName = "J.K. Rowling", AuthorBio = "British author best known for writing the Harry Potter series." },
                new Author { AuthorID = 2, AuthorName = "Stephen King", AuthorBio = "American author known for his horror, supernatural fiction, and suspense novels." }
            );

            modelBuilder.Entity<Book>().HasData(
               new Book { BookID = 1, PublishedDate = new DateTime(1997, 6, 26), Title = "Harry Potter and the Philosopher's Stone", AvailableCopies = 10, TotalCopies = 10, ISBN = "9780747532743", AuthorID = 1 },
               new Book { BookID = 2, PublishedDate = new DateTime(1986, 9, 15), Title = "It", AvailableCopies = 8, TotalCopies = 8, ISBN = "9780451169518", AuthorID = 2 }
           );

           modelBuilder.Entity<Member>().HasData(
               new Member { MemberID = 1, PhoneNumber = "1234567890", FirstName = "John", LastName = "Doe", RegistrationDate = DateTime.Now, Email = "john.doe@example.com" },
               new Member { MemberID = 2, PhoneNumber = "9876543210", FirstName = "Jane", LastName = "Smith", RegistrationDate = DateTime.Now, Email = "jane.smith@example.com" }
               // Add more members as needed
           );
        }

    }
}