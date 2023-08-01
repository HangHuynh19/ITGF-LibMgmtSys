using LibMgmtSys.Backend.Domain.BookReviewAggregate;
using LibMgmtSys.Backend.Domain.BookReviewAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
  public interface IBookReviewRepository
  {
    Task<List<BookReview>> GetAllBookReviewsAsync();
    Task<BookReview?> GetBookReviewByIdAsync(BookReviewId id);
    Task<List<BookReview>> GetBookReviewsByIdsAsync(List<BookReviewId> ids);
    Task AddBookReviewAsync(BookReview bookReview);
    Task UpdateBookReviewAsync(BookReview bookReview);
    Task DeleteBookReviewAsync(BookReview bookReview);
  }
}