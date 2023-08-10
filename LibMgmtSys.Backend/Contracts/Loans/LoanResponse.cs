namespace Contracts.Loans
{
    public record LoanResponse(
        string LoanId,
        string BookId,
        string BookTitle,
        string CustomerId,
        string CustomerEmail,
        string LoanedAt,
        string DueDate,
        string ReturnedAt);
}

