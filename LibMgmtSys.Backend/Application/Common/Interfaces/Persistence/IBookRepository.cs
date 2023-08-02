using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(BookId id);
        Task<List<Book>> GetBooksByIdsAsync(List<BookId> ids);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
    }
}