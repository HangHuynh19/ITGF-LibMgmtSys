using Ardalis.SmartEnum;

namespace LibMgmtSys.Backend.Domain.UserAggregate.Enum
{
    public class Role : SmartEnum<Role>
    {
        public static readonly Role Admin = new(nameof(Admin).ToLower(), 1);
        public static readonly Role Customer = new(nameof(Customer).ToLower(), 2);

        private Role() : base("", 0)
        {
        }
        private Role(string name, int value) : base(name, value)
        {
        }
    }
}