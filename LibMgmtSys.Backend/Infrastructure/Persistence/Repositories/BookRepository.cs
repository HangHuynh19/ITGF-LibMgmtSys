using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
  public class BookRepository : IBookRepository
  {
    private readonly LibMgmtSysDbContext _dbContext;

    public BookRepository(LibMgmtSysDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
      return await _dbContext.Books.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(BookId id)
    {
      return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<List<Book>> GetBooksByIdsAsync(List<BookId> ids)
    {
      return await _dbContext.Books.Where(book => ids.Contains(book.Id)).ToListAsync();
    }

    public async Task<Book> AddBookAsync(Book book)
    {
      _dbContext.Add(book);
      await _dbContext.SaveChangesAsync();
      return book;
    }

    public async Task UpdateBookAsync(Book book)
    {
      _dbContext.Update(book);
      await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Book book)
    {
      _dbContext.Remove(book);
      await _dbContext.SaveChangesAsync();
    }
  }
}