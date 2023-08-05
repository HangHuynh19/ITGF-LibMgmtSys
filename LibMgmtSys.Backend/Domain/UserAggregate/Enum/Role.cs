using Ardalis.SmartEnum;

namespace LibMgmtSys.Backend.Domain.UserAggregate.Enum
{
    public class Role : SmartEnum<Role>
    {
        private Role(string name, int value) : base(name, value)
        {
        }
        
        public static readonly Role Admin = new(nameof(Admin).ToLower(), 1);
        public static readonly Role Customer = new(nameof(Customer).ToLower(), 2);
    }
    /*public enum Role
    {
        Admin,
        Customer
    }*/
}