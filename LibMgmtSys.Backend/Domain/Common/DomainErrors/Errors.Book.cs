using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
  public static partial class Errors 
  {
    public static class Book 
    {
      public static Error AuthorNotFound => Error.Validation(
        code: "Book.InvalidAuthorId", 
        description: "Author with given ID does not exist"
      );
    }
  }
}