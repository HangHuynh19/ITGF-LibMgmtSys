using LibMgmtSys.Backend.Application.Authentication.Common;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;

namespace LibMgmtSys.Backend.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository
            )
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
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

            await _userRepository.AddUserAsync(user);

            var jwtToken = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, jwtToken);
        }
    }
}

