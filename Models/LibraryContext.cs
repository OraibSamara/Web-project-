using Microsoft.EntityFrameworkCore;

namespace LibraryProj.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        { }

		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Administrator",
                    Username = "admin",
                    Password = "admin",
                }
                );
       

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Oraib" },
                new Author { AuthorId = 2, Name = "Hamza" },
                new Author { AuthorId = 3, Name = "Ashraf" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Name = "Test 1",
                    Edition = "First",
                    AuthorId = 1,

                },
                new Book
                {
                    BookId = 2,
                    Name = "Test 2",
                    Edition = "Second",
                    AuthorId = 2,
                },
                new Book
                {
                    BookId = 3,
                    Name = "Test 3",
                    Edition = "Third",
                    AuthorId = 3,
                }
                );

        }
    }
}
