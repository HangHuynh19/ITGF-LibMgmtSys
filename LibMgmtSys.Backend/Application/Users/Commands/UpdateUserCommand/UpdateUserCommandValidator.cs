using FluentValidation;

namespace LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(updateUserCommand => updateUserCommand.FirstName)
                .MaximumLength(100);
            RuleFor(updateUserCommand => updateUserCommand.LastName)
                .MaximumLength(100);
            RuleFor(updateUserCommand => updateUserCommand.Email)
                .MaximumLength(100);
            RuleFor(updateUserCommand => updateUserCommand.Password)
                .MaximumLength(100);
        }
    }
}
