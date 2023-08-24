using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateUserCommandHandler(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(UserId.Create(command.Id));
            var customer = await _unitOfWork.Customer.GetCustomerByUserIdAsync(UserId.Create(command.Id));
            
            if (user is null)
            {
                return Errors.User.UserNotFound;
            }
            
            var newPassword = string.IsNullOrEmpty(command.Password) ? user.Password : command.Password;

            user.UpdateUserProperties(
                command.FirstName,
                command.LastName,
                command.Email,
                newPassword
            );
            
            await _unitOfWork.User.UpdateUserAsync(user);
            
            if (customer is not null)
            {
                customer.UpdateCustomerProperties(
                    command.FirstName,
                    command.LastName,
                    command.Email
                );
                await _unitOfWork.Customer.UpdateCustomerAsync(customer);
            }

            return user;
        }
    }
}