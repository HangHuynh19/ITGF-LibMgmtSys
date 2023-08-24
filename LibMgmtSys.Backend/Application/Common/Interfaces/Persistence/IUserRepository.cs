using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(UserId id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> AddUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}

