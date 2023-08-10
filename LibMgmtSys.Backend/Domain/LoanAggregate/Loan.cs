using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.LoanAggregate
{
  public class Loan : AggregateRoot<LoanId>
  {
    public BookId BookId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public DateTime LoanedAt { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnedAt { get; private set; }
    public Customer Customer { get; private set; }
    public Book Book { get; private set; }
    private Loan() : base(LoanId.CreateUnique())
    {
    }

    private Loan(
      LoanId loanId,
      BookId bookId,
      CustomerId customerId,
      DateTime loanedAt,
      DateTime dueDate,
      DateTime? returnedAt
      ) : base(loanId)
    {
      BookId = bookId;
      CustomerId = customerId;
      LoanedAt = loanedAt;
      DueDate = dueDate;
      ReturnedAt = returnedAt;
    }

    public static Loan Create(
      BookId bookId,
      CustomerId customerId,
      DateTime loanedAt,
      DateTime dueDate,
      DateTime? returnedAt = null
      )
    {
      return new Loan(
        LoanId.CreateUnique(),
        bookId,
        customerId,
        loanedAt.ToUniversalTime(),
        dueDate.ToUniversalTime(),
        returnedAt
        );
    }
  }
}