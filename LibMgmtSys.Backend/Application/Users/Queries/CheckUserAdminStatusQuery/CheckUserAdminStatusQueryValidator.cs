using FluentValidation;

namespace LibMgmtSys.Backend.Application.Users.Queries.CheckUserAdminStatusQuery
{
    public class CheckUserAdminStatusQueryValidator : AbstractValidator<CheckUserAdminStatusQuery>
    {
        public CheckUserAdminStatusQueryValidator()
        {
            RuleFor(checkUserAdminStatusCommand => checkUserAdminStatusCommand.UserId)
                .NotEmpty();
        }
    }
}