using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByIdQuery
{
    public record GetCustomerByIdQuery(CustomerId CustomerId) : IRequest<ErrorOr<Customer>>;
}