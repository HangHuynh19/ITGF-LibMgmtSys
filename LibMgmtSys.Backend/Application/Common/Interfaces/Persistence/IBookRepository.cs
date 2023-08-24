using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksWithPaginationAsync(int pageNumber, int pageSize, string sortOrder, string searchTerm);
        Task<Book?> GetBookByIdAsync(BookId id);
        Task<List<Book>> GetBooksByIdsAsync(List<BookId> ids);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        void DeleteBook(Book book);
    }
}