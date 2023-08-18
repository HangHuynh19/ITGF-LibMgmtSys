using Contracts.Customers;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authorization;
using LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByIdQuery;
using LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByUserIdQuery;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
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
        private readonly IJwtTokenDecoder _jwtTokenDecoder;
        
        public CustomersController(IMapper mapper, ISender mediator, IJwtTokenDecoder jwtTokenDecoder)
        {
            _mapper = mapper;
            _mediator = mediator;
            _jwtTokenDecoder = jwtTokenDecoder;
        }
        
        [HttpGet("profile")]
        public async Task<IActionResult> GetCustomerProfile(
            [FromHeader(Name = "Authorization")] string authorization)
        {
            var bearerToken = _jwtTokenDecoder.GetBearerTokenFromHeader(authorization);
            
            if (bearerToken is null)
            {
                return Unauthorized();
            }
            
            var userFromToken = _jwtTokenDecoder.DecodeJwtToken(bearerToken);
            var getCustomerByUserIdQuery = new GetCustomerByUserIdQuery(UserId.Create(userFromToken.UserId));
            var getCustomerByIdResult = await _mediator.Send(getCustomerByUserIdQuery);
            
            return getCustomerByIdResult.Match(
                customer => Ok(_mapper.Map<CustomerResponse>(customer)),
                errors => Problem(errors));
        }
        
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
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