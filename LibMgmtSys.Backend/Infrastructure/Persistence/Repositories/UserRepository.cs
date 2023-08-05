using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibMgmtSysDbContext _dbContext;
        
        public UserRepository(LibMgmtSysDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
        
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        
        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}

