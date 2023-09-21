using Microsoft.EntityFrameworkCore;
using MinimalAPI_BookStore.Models;

namespace MinimalAPI_BookStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Description = "A story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan.",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Classic",
                    Year = new DateTime(1925, 1, 1),
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "The story of racial injustice and the loss of innocence in a Southern town.",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    Year = new DateTime(1960, 1, 1),
                    IsAvailable = true
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Description = "A dystopian novel set in a totalitarian society ruled by the Party and its leader, Big Brother.",
                    Author = "George Orwell",
                    Genre = "Science Fiction",
                    Year = new DateTime(1949, 1, 1),
                    IsAvailable = true
                },
                new Book
                {
                    Id = 4,
                    Title = "Pride and Prejudice",
                    Description = "The romantic entanglements of the Bennet sisters in early 19th century England.",
                    Author = "Jane Austen",
                    Genre = "Romance",
                    Year = new DateTime(1813, 1, 1),
                    IsAvailable = true
                },
                new Book
                {
                    Id = 5,
                    Title = "The Hobbit",
                    Description = "The adventure of Bilbo Baggins as he sets out on a quest to help a group of dwarves reclaim their homeland.",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    Year = new DateTime(1937, 1, 1),
                    IsAvailable = true
                });
        }
    }
}

