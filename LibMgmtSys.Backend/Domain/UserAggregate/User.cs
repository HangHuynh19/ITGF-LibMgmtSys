using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.UserAggregate.Enum;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private User(
          UserId userId,
          string firstName,
          string lastName,
          string email,
          string password,
          Role role
          ) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }

        public static User CreateCustomer(
          string firstName,
          string lastName,
          string email,
          string password
          )
        {
            return new User(
              UserId.CreateUnique(),
              firstName,
              lastName,
              email,
              password,
              Role.Customer
              );
        }

        public static User CreateAdmin(
          string firstName,
          string lastName,
          string email,
          string password
          )
        {
            return new User(
              UserId.CreateUnique(),
              firstName,
              lastName,
              email,
              password,
              Role.Admin
              );
        }
    }
}