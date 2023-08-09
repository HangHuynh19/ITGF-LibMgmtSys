using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LibMgmtSysDbContext _dbContext;
        
        public CustomerRepository(LibMgmtSysDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Customer?> GetCustomerByIdAsync(CustomerId customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(customerId));
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(UserId userId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.UserId.Equals(userId));
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}

