namespace LibMgmtSys.Backend.Contracts.Books
{
  public record CreateAuthorRequest(
    string Name,
    string Biography
  );
}