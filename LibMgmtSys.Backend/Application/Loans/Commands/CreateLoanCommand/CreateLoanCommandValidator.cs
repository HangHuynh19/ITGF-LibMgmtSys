using FluentValidation;

namespace LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(createLoanCommand => createLoanCommand.CustomerId)
                .NotEmpty();
            RuleFor(createLoanCommand => createLoanCommand.BookIds)
                .NotEmpty();
        }
    }
}

