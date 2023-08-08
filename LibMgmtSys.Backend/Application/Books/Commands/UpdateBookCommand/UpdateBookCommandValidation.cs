using FluentValidation;

namespace LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand
{
    public class UpdateBookCommandValidation : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidation()
        {
            RuleFor(updateBookCommand => updateBookCommand.Title)
                .MaximumLength(100);
            RuleFor(updateBookCommand => updateBookCommand.Isbn)
                .MaximumLength(13);
            RuleFor(updateBookCommand => updateBookCommand.Publisher)
                .MaximumLength(100);
            RuleFor(updateBookCommand => updateBookCommand.Year)
                .InclusiveBetween(0, 9999);
            RuleFor(updateBookCommand => updateBookCommand.Description)
                .MaximumLength(1000);
            RuleFor(updateBookCommand => updateBookCommand.Image)
                .NotEmpty();
            RuleFor(updateBookCommand => updateBookCommand.BorrowingPeriod)
                .InclusiveBetween(0, 9999);
            RuleFor(updateBookCommand => updateBookCommand.Quantity)
                .InclusiveBetween(0, 9999);
        }
    }
}
