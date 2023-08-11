using Contracts.Customers;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class CustomerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, CustomerResponse>()
                .Map(dest => dest.Id, src => src.Id.Value.ToString())
                .Map(dest => dest.LoanIds, src => src.Loans.Select(loan => loan.Id.Value.ToString()));
        }
    }
}
