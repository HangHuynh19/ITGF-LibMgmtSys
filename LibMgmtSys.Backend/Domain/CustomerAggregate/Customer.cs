using LibMgmtSys.Backend.Domain.BillAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.CustomerAggregate
{
  public sealed class Customer : AggregateRoot<CustomerId>
  {
    private readonly List<Loan> _loans = new();
    private readonly List<Bill> _bills = new();
    public string FirstName { get; }
    public string LastName { get; }
    public Uri ProfileImage { get; }
    public UserId UserId { get; }
    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();
    public IReadOnlyList<Bill> Bills => _bills.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    private Customer(
      string firstName,
      string lastName,
      Uri profileImage,
      UserId userId,
      CustomerId? customerId = null
      ) : base(customerId ?? CustomerId.CreateUnique())
    {
      FirstName = firstName;
      LastName = lastName;
      ProfileImage = profileImage;
      UserId = userId;
    }

    public static Customer Create(
      string firstName,
      string lastName,
      Uri profileImage,
      UserId userId
      )
    {
      return new Customer(
        firstName,
        lastName,
        profileImage,
        userId
        );
    }
  }
}