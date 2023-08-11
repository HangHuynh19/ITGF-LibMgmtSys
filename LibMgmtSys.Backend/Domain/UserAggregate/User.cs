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
        //public DateTime CreatedAt { get; private set; }
        //public DateTime UpdatedAt { get; private set; }

        private User() : base(UserId.CreateUnique())
        {
        }

        private User(
          string firstName,
          string lastName,
          string email,
          string password,
          Role role, 
          UserId? userId = null
          ) : base(userId ?? UserId.CreateUnique())
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
              firstName,
              lastName,
              email,
              password,
              Role.Admin
              );
        }
        
        public void UpdateUserProperties(
          string firstName,
          string lastName,
          string email,
          string password
          )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}