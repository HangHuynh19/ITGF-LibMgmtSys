using LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand;
using LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand;
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
        
        public UsersController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserRequest request)
        {
            var updateUserCommand = _mapper.Map<UpdateUserCommand>((request, id));
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

