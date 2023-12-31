using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
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
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
            return user;
        }
        
        public async Task<User?> GetUserByIdAsync(UserId id)
        {
            var input = id.Value.ToString();
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }
        
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        
        public async Task<User> AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            //await _dbContext.SaveChangesAsync();
            return user;
        }
        
        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            //await _dbContext.SaveChangesAsync();
        }
        
        public void DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            //await _dbContext.SaveChangesAsync();
        }
    }
}

