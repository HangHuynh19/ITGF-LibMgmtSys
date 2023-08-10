using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
    public static partial class Errors
    {
        public static class Customer
        {
            public static Error CustomerNotFound = Error.Validation(
                code: "Customer.InvalidCustomerId",
                description: "Customer with given ID does not exist"
            );
        }
    }
}

