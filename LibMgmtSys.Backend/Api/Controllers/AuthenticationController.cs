using Contracts.Authentication;
using LibMgmtSys.Backend.Application.Authentication.Commands.Register;
using LibMmgtSys.Backend.Api.Controllers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        
        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var authResult = await _mediator.Send(command);
            
            return authResult.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                errors => Problem(errors));
        }
    }
}