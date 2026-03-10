using Microsoft.EntityFrameworkCore;
using WebClient.Models;

namespace WebClient.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly AppDbContext _context;

        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher?> UpdatePublisherAsync(int id, Publisher publisher)
        {
            var existingPublisher = await _context.Publishers.FindAsync(id);
            if (existingPublisher == null)
            {
                return null;
            }

            existingPublisher.PublisherName = publisher.PublisherName;
            existingPublisher.City = publisher.City;
            existingPublisher.State = publisher.State;
            existingPublisher.Country = publisher.Country;

            _context.Publishers.Update(existingPublisher);
            await _context.SaveChangesAsync();

            return existingPublisher;
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return false;
            }

            var hasBooks = await _context.Books.AnyAsync(b => b.PubId == id);
            if (hasBooks)
            {
                throw new InvalidOperationException("Cannot delete this publisher because it has associated books.");
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
