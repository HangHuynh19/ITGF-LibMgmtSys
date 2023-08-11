using Contracts.Customers;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class CustomerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, CustomerResponse>()
                .Map(dest => dest.Id, src => src.Id.Value.ToString())
                .Map(dest => dest.BookLoans, src => src.Loans.Select(loan => loan.Book.Title));
        }
    }
}
