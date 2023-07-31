using LibMgmtSys.Backend.Domain.BillAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.BillAggregate
{
  public class Bill : AggregateRoot<BillId>
  {
    public LoanId LoanId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public Customer Customer { get; private set; }
    private Bill() : base(BillId.CreateUnique())
    {
    }
    
    private Bill(
      BillId billId,
      LoanId loanId,
      decimal amount,
      DateTime dueDate
      ) : base(billId)
    {
      LoanId = loanId;
      Amount = amount;
      DueDate = dueDate;
    }

    public static Bill Create(
      LoanId loanId,
      decimal amount,
      DateTime dueDate
      )
    {
      return new Bill(
        BillId.CreateUnique(),
        loanId,
        amount,
        dueDate
        );
    }
  }
}