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
        private readonly ICustomerRepository _customerRepository;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            ICustomerRepository customerRepository
        )
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }
        
        public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(UserId.Create(command.Id));
            var customer = await _customerRepository.GetCustomerByUserIdAsync(UserId.Create(command.Id));
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

            await _userRepository.UpdateUserAsync(user);
            
            if (customer is not null)
            {
                customer.UpdateCustomerProperties(
                    command.FirstName,
                    command.LastName,
                    command.Email
                );
                await _customerRepository.UpdateCustomerAsync(customer);
            }

            return user;
        }
    }
}