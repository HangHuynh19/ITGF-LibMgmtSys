using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.Common.ValueObjects
{
    public sealed class DecodedJwtToken : ValueObject
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Role { get; private set; }
        
        private DecodedJwtToken(Guid userId, string firstName, string lastName, string role)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
        
        public static DecodedJwtToken Create(Guid userId, string firstName, string lastName, string role)
        {
            return new DecodedJwtToken(userId, firstName, lastName, role);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return FirstName;
            yield return LastName;
            yield return Role;
        }
    }
}

