using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authorization;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }
        
        public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(UserId.Create(command.Id));
            
            if (user is null)
            {
                return Errors.User.UserNotFound;
            }

            user.UpdateUserProperties(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Password
            );

            await _userRepository.UpdateUserAsync(user);

            return user;
        }
    }
}