using Microsoft.EntityFrameworkCore;
using WebClient.Models;

namespace WebClient.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> CreateBookAsync(BookCreateUpdateDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Type = dto.Type,
                PubId = dto.PubId,
                Price = dto.Price,
                Advance = dto.Advance,
                Royalty = dto.Royalty,
                YtdSales = dto.YtdSales,
                Notes = dto.Notes,
                PublishedDate = dto.PublishedDate ?? DateTime.Now 
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Link authors
            if (dto.AuthorIds != null && dto.AuthorIds.Any())
            {
                foreach (var authorId in dto.AuthorIds)
                {
                    _context.BookAuthors.Add(new BookAuthor { BookId = book.BookId, AuthorId = authorId });
                }
                await _context.SaveChangesAsync();
            }

            return book;
        }

        public async Task<Book?> UpdateBookAsync(int id, BookCreateUpdateDto dto)
        {
            var existingBook = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = dto.Title;
            existingBook.Type = dto.Type;
            existingBook.PubId = dto.PubId;
            existingBook.Price = dto.Price;
            existingBook.Advance = dto.Advance;
            existingBook.Royalty = dto.Royalty;
            existingBook.YtdSales = dto.YtdSales;
            existingBook.Notes = dto.Notes;
            existingBook.PublishedDate = dto.PublishedDate ?? existingBook.PublishedDate ?? DateTime.Now;

            // Update Many-to-Many
            _context.BookAuthors.RemoveRange(existingBook.BookAuthors);
            await _context.SaveChangesAsync();

            if (dto.AuthorIds != null && dto.AuthorIds.Any())
            {
                foreach (var authorId in dto.AuthorIds)
                {
                    _context.BookAuthors.Add(new BookAuthor { BookId = id, AuthorId = authorId });
                }
            }

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string keyword)
        {
            var query = _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var lowerKeyword = keyword.ToLower();
                query = query.Where(b => 
                    b.Title.ToLower().Contains(lowerKeyword) || 
                    b.BookAuthors.Any(ba => ba.Author.EmailAddress.ToLower().Contains(lowerKeyword)));
            }

            return await query.ToListAsync();
        }
    }
}
