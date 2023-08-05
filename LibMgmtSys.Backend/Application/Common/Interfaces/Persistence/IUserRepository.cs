using LibMgmtSys.Backend.Domain.UserAggregate;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}

