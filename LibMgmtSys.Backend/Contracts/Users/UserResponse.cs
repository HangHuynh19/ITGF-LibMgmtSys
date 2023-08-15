namespace LibMgmtSys.Backend.Contracts.Users
{
    public record UserResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email);
}

