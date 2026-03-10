using WebClient.Models;

namespace WebClient.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(BookCreateUpdateDto bookDto);
        Task<Book?> UpdateBookAsync(int id, BookCreateUpdateDto bookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string keyword);
    }
}
