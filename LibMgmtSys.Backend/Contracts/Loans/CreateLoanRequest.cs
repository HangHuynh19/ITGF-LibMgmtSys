namespace Contracts.Loans
{
    public record CreateLoanRequest(
        List<Guid> BookIds,
        DateTime LoanedAt,
        Guid CustomerId);
}

