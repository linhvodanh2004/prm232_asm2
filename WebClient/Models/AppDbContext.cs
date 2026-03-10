using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace WebClient.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.AuthorId, ba.BookId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // Fix decimal warnings
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Book>()
                .Property(b => b.Advance)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Book>()
                .Property(b => b.Royalty)
                .HasColumnType("decimal(18,2)");

            // Seed Data setup
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PubId = 1, PublisherName = "Tech Books Publishing", City = "New York", State = "NY", Country = "USA" },
                new Publisher { PubId = 2, PublisherName = "Education Press", City = "London", State = "LDN", Country = "UK" }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@example.com", Phone = "123-456-7890", Address = "123 Main St", City = "New York", State = "NY", Zip = "10001" },
                new Author { AuthorId = 2, FirstName = "Jane", LastName = "Smith", EmailAddress = "jane.smith@example.com", Phone = "987-654-3210", Address = "456 Broad St", City = "London", State = "LDN", Zip = "SW1A" }
            );

            // Using explicit date time formatting for the DateTime seed
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Mastering ASP.NET Core 8", Type = "Technology", PubId = 1, Price = 45.99m, Advance = 1000m, Royalty = 10m, YtdSales = 500, Notes = "Comprehensive guide", PublishedDate = new System.DateTime(2023, 10, 1) },
                new Book { BookId = 2, Title = "Entity Framework Core in Action", Type = "Technology", PubId = 1, Price = 39.99m, Advance = 500m, Royalty = 12m, YtdSales = 300, Notes = "Deep dive into EF Core", PublishedDate = new System.DateTime(2024, 1, 15) },
                new Book { BookId = 3, Title = "The Art of Programming", Type = "Education", PubId = 2, Price = 55.00m, Advance = 2000m, Royalty = 15m, YtdSales = 1200, Notes = "Classic computer science book", PublishedDate = new System.DateTime(2022, 5, 20) }
            );

            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { AuthorId = 1, BookId = 1 },
                new BookAuthor { AuthorId = 1, BookId = 2 },
                new BookAuthor { AuthorId = 2, BookId = 3 }
            );
        }
    }
}
