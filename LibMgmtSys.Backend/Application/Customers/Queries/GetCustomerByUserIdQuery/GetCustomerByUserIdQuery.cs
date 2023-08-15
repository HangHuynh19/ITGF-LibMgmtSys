using ErrorOr;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByUserIdQuery
{
    public record GetCustomerByUserIdQuery(UserId UserId) : IRequest<ErrorOr<Customer>>;
}

