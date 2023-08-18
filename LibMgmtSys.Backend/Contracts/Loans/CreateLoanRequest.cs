namespace Contracts.Loans
{
    public record CreateLoanRequest(
        List<string> BookIds,
        DateTime LoanedAt);
}

