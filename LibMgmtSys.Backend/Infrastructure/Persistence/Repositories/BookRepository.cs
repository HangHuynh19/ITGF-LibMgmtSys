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

    public async Task<List<Book>> GetAllBooksWithPaginationAsync(
      int pageNumber, 
      int pageSize, 
      string sortOrder, 
      string searchTerm)
    {
      /*return await _dbContext.Books
        .Include(book => book.Authors)
        .Include(genre => genre.Genres)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();*/

      IQueryable<Book> query = _dbContext.Books
        .Include(book => book.Authors)
        .Include(book => book.Genres);
      
      if (!string.IsNullOrEmpty(searchTerm))
      {
        query = query.Where(book =>
          book.Title.ToLower().Contains(searchTerm) ||
          book.Authors.Any(author => author.Name.ToLower().Contains(searchTerm)) ||
          book.Description.ToLower().Contains(searchTerm) ||
          book.Publisher.ToLower().Contains(searchTerm) ||
          book.Genres.Any(genre => genre.Name.ToLower().Contains(searchTerm)));
      }

      query = sortOrder switch
      {
        "desc" => query.OrderByDescending(book => book.Title),
        _ => query.OrderBy(book => book.Title)
      };

      query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
      
      return await query.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(BookId id)
    {
      return await _dbContext.Books
        .Include(book => book.Authors)
        .Include(book => book.Genres)
        .FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<List<Book>> GetBooksByIdsAsync(List<BookId> ids)
    {
      return await _dbContext.Books
        .Include(book => book.Authors)
        .Include(book => book.Genres)
        .Where(book => ids.Contains(book.Id)).ToListAsync();
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