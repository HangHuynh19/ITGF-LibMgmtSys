using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
  public static partial class Errors 
  {
    public static class Author 
    {
      public static Error BookNotFound => Error.Validation(
        code: "Author.InvalidBookId", 
        description: "Book with given ID does not exist"
      );
    }
  }
}