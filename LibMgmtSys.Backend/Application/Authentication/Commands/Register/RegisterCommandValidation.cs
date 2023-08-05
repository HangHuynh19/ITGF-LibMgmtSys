using FluentValidation;

namespace LibMgmtSys.Backend.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(50);
        }
    }
}

