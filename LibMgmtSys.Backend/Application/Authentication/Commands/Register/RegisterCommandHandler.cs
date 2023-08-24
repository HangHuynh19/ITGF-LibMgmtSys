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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
        public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.User.GetUserByEmailAsync(command.Email) is not null)
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

            await _unitOfWork.User.AddUserAsync(user);
            await _unitOfWork.Customer.AddCustomerAsync(customer);
            
            var jwtToken = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, jwtToken);
        }
    }
}

