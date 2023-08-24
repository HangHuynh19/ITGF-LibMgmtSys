using LibMgmtSys.Backend.Domain.UserAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteUserCommandHandler(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(request.UserId);
            
            if (user is null)
            {
                return Errors.User.UserNotFound;
            }
            
            await _unitOfWork.User.DeleteUserAsync(user);
            return user;
        }
    }
}