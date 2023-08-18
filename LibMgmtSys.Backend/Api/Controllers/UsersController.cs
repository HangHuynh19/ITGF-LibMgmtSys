using LibMgmtSys.Backend.Application.Common.Interfaces.Authorization;
using LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand;
using LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand;
using LibMgmtSys.Backend.Application.Users.Queries.CheckUserAdminStatusQuery;
using LibMgmtSys.Backend.Contracts.Users;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using LibMmgtSys.Backend.Api.Controllers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibMgmtSys.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly IJwtTokenDecoder _jwtTokenDecoder;
        
        public UsersController(IMapper mapper, ISender mediator, IJwtTokenDecoder jwtTokenDecoder)
        {
            _mapper = mapper;
            _mediator = mediator;
            _jwtTokenDecoder = jwtTokenDecoder;
        }

        [HttpGet("check-admin-status")]
        public async Task<IActionResult> CheckUserAdminStatus([FromHeader(Name = "Authorization")] string authorization)
        {
            var bearerToken = _jwtTokenDecoder.GetBearerTokenFromHeader(authorization);
            
            if (bearerToken is null)
            {
                return Unauthorized();
            }
            
            var userFromToken = _jwtTokenDecoder.DecodeJwtToken(bearerToken);
            var isAdmin = await _mediator.Send(new CheckUserAdminStatusQuery(userFromToken.UserId));
            
            return isAdmin.Match(
                admin => Ok(admin),
                errors => Problem(errors));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
            //[FromRoute] string id, 
            [FromBody] UpdateUserRequest request, 
            [FromHeader(Name = "Authorization")] string authorization)
        {
            var bearerToken = _jwtTokenDecoder.GetBearerTokenFromHeader(authorization);
            
            if (bearerToken is null)
            {
                return Unauthorized();
            }
            
            var decodedJwtToken = _jwtTokenDecoder.DecodeJwtToken(bearerToken);
            
            /*if (!decodedJwtToken.UserId.ToString().Equals(id))
            {
                return Unauthorized();
            } */
            
            var updateUserCommand = _mapper.Map<UpdateUserCommand>((request, decodedJwtToken.UserId.ToString()));
            var updateUserResult = await _mediator.Send(updateUserCommand);
            
            return updateUserResult.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var deleteUserCommand = new DeleteUserCommand(UserId.Create(Guid.Parse(id)));
            var deleteUserResult = await _mediator.Send(deleteUserCommand);

            return deleteUserResult.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }
    }
}

