using LibMgmtSys.Backend.Application.Authentication.Common;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate;

namespace LibMgmtSys.Backend.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public RegisterCommandHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            ICustomerRepository customerRepository
            )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }
            
            var user = User.CreateCustomer(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Password
                );

            
            var customer = Customer.Create(
                command.FirstName,
                command.LastName,
                command.Email,
                user.Id
            );

            await _userRepository.AddUserAsync(user);
            await _customerRepository.AddCustomerAsync(customer);
            var jwtToken = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, jwtToken);
        }
    }
}

