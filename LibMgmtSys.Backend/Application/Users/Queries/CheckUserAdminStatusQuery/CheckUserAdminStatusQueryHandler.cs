using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using LibMgmtSys.Backend.Domain.UserAggregate.Enum;
using MediatR;

namespace LibMgmtSys.Backend.Application.Users.Queries.CheckUserAdminStatusQuery
{
    public class CheckUserAdminStatusQueryHandler : IRequestHandler<CheckUserAdminStatusQuery, ErrorOr<bool>>
    {
        private readonly IUserRepository _userRepository;
        
        public CheckUserAdminStatusQueryHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }
        
        public async Task<ErrorOr<bool>> Handle(CheckUserAdminStatusQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(UserId.Create(query.UserId));
            
            if (user is null)
            {
                return Errors.User.UserNotFound;
            }

            return user.Role == Role.Admin;
        }
    }
}
