using LibMgmtSys.Backend.Domain.BillAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.CustomerAggregate
{
  public sealed class Customer : AggregateRoot<CustomerId>
  {
    private readonly List<Loan> _loans = new();
    private readonly List<Bill> _bills = new();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public Uri? ProfileImage { get; private set; } = new Uri("https://i.imgur.com/1qk9n0z.png");
    public UserId UserId { get; private set; }
    public User User { get; private set; }
    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();
    public IReadOnlyList<Bill> Bills => _bills.AsReadOnly();
    //public DateTime CreatedAt { get; }
    //public DateTime UpdatedAt { get; }

    private Customer() : base(CustomerId.CreateUnique())
    {
    }
    
    private Customer(
      CustomerId customerId,
      string firstName,
      string lastName,
      string email,
      UserId userId,
      Uri? profileImage = null
      ) : base(customerId)
    {
      FirstName = firstName;
      LastName = lastName;
      Email = email;
      UserId = userId;
      profileImage ??= new Uri("https://i.imgur.com/1qk9n0z.png");
    }

    public static Customer Create(
      string firstName,
      string lastName,
      string email,
      UserId userId
      )
    {
      return new Customer(
        CustomerId.CreateUnique(),
        firstName,
        lastName,
        email,
        userId
        );
    }
  }
}