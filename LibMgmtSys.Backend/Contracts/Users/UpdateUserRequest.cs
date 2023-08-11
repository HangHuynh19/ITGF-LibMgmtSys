namespace LibMgmtSys.Backend.Contracts.Users
{
    public record UpdateUserRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}