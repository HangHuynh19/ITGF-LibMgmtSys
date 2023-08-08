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

    public async Task<List<Book>> GetAllBooksWithPaginationAsync(int page, int pageSize)
    {
      return await _dbContext.Books
        .Include(book => book.Authors)
        .Include(genre => genre.Genres)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
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

    public async Task<Book> UpdateBookAsync(Book book)
    {
      var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
      
      bookInDb.UpdateBookProperties(
        book.Title,
        book.Isbn,
        book.Publisher,
        book.Year,
        book.Description,
        book.Image,
        book.BorrowingPeriod,
        book.Quantity
      );
      await _dbContext.Entry(bookInDb).Collection(b => b.Authors).LoadAsync();
      await _dbContext.Entry(bookInDb).Collection(b => b.Genres).LoadAsync();
      _dbContext.Update(bookInDb);
      await _dbContext.SaveChangesAsync();
      
      return book;
    }

    public async Task DeleteBookAsync(Book book)
    {
      _dbContext.Remove(book);
      await _dbContext.SaveChangesAsync();
    }
  }
}