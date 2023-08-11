using FluentValidation;

namespace LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(deleteUserCommand => deleteUserCommand.UserId)
                .NotEmpty();
        }
    }
}

