using LibMgmtSys.Backend.Domain.LoanAggregate;

namespace Contracts.Customers
{
    public record CustomerResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        Uri ProfileImage,
        List<string> BookLoans);
}
