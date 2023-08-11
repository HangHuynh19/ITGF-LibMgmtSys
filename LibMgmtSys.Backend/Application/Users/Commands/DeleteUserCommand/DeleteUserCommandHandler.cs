using LibMgmtSys.Backend.Domain.UserAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository;
        
        public DeleteUserCommandHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }
        
        public async Task<ErrorOr<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            
            if (user is null)
            {
                return Errors.User.UserNotFound;
            }
            
            await _userRepository.DeleteUserAsync(user);
            
            return user;
        }
    }
}