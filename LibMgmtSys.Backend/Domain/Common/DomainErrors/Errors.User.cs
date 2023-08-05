using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error UserNotFound = Error.NotFound(
                code: "User.NotFound",
                description: "User with given ID does not exist"
            );
            
            public static Error DuplicateEmail = Error.Conflict(code: "User.DuplicateEmail", description: "User with given email already exists.");
        }
    }
}

