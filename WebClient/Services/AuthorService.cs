using Microsoft.EntityFrameworkCore;
using WebClient.Models;

namespace WebClient.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> UpdateAuthorAsync(int id, Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.LastName = author.LastName;
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.Phone = author.Phone;
            existingAuthor.Address = author.Address;
            existingAuthor.City = author.City;
            existingAuthor.State = author.State;
            existingAuthor.Zip = author.Zip;
            existingAuthor.EmailAddress = author.EmailAddress;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();

            return existingAuthor;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
