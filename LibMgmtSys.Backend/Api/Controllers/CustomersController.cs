using Contracts.Customers;
using LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByIdQuery;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibMmgtSys.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        
        public CustomersController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerById([FromRoute] string id)
        {
            var getCustomerByIdQuery = new GetCustomerByIdQuery(CustomerId.Create(Guid.Parse(id)));
            var getCustomerByIdResult = await _mediator.Send(getCustomerByIdQuery);
            
            return getCustomerByIdResult.Match(
                customer => Ok(_mapper.Map<CustomerResponse>(customer)),
                errors => Problem(errors));
        }
    }
}