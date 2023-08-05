using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
  public static partial class Errors 
  {
    public static class Author 
    {
      public static Error AuthorNotFound = Error.Validation(
        code: "Author.InvalidAuthorId", 
        description: "Author with given ID does not exist"
      );
    }
  }
}