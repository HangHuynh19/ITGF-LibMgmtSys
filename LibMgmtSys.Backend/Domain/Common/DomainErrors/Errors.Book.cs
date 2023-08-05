using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
  public static partial class Errors 
  {
    public static class Book 
    {
      public static Error BookNotFound = Error.Validation(
        code: "Book.InvalidBookId", 
        description: "Book with given ID does not exist"
      );
    }
  }
}