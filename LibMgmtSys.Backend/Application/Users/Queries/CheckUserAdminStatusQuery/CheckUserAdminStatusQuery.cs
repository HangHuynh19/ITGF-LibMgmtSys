using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Users.Queries.CheckUserAdminStatusQuery
{
    public record CheckUserAdminStatusQuery(
        Guid UserId
    ) : IRequest<ErrorOr<bool>>;
}

