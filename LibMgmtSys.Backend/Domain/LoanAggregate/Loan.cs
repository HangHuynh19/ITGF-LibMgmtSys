using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.LoanAggregate
{
  public class Loan : AggregateRoot<LoanId>
  {
    public BookId BookId { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime LoanedAt { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnedAt { get; private set; }

    private Loan(
      LoanId loanId,
      BookId bookId,
      UserId userId,
      DateTime loanedAt,
      DateTime dueDate,
      DateTime? returnedAt
      ) : base(loanId)
    {
      BookId = bookId;
      UserId = userId;
      LoanedAt = loanedAt;
      DueDate = dueDate;
      ReturnedAt = returnedAt;
    }

    public static Loan Create(
      BookId bookId,
      UserId userId,
      DateTime loanedAt,
      DateTime dueDate,
      DateTime? returnedAt,
      LoanId? loanId = null
      )
    {
      return new Loan(
        loanId ?? LoanId.CreateUnique(),
        bookId,
        userId,
        loanedAt,
        dueDate,
        returnedAt
        );
    }
  }
}