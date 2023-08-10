using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
    public static partial class Errors
    {
        public static class Loan
        {
            public static Error LoanNotFound = Error.Validation(
                code: "Loan.InvalidLoanId",
                description: "Loan with given ID does not exist"
            );
        }
    }
}

