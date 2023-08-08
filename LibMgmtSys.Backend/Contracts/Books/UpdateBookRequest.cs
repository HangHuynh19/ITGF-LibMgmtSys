namespace LibMgmtSys.Backend.Contracts.Books
{
    public record UpdateBookRequest
    (
        string Title,
        string Isbn,
        string Publisher,
        //List<Guid> AuthorIds,
        int Year,
        //List<Guid> GenreIds,
        string Description,
        Uri Image,
        int BorrowingPeriod,
        int Quantity
        //List<BookReviewId> BookReviewIds
    );
}