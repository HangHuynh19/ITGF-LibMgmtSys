using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(CustomerId customerId);
        Task<Customer?> GetCustomerByUserIdAsync(UserId userId);
        Task<Customer> AddCustomerAsync(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}

