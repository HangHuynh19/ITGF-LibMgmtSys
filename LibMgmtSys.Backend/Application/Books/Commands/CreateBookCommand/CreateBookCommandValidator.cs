using FluentValidation;

namespace LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand
{
  public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
  {
    public CreateBookCommandValidator()
    {
      RuleFor(createBookCommand => createBookCommand.Title)
        .NotEmpty()
        .MaximumLength(100);
      RuleFor(createBookCommand => createBookCommand.Isbn)
        .NotEmpty()
        .MaximumLength(13);
      RuleFor(createBookCommand => createBookCommand.Publisher)
        .NotEmpty()
        .MaximumLength(100);
      RuleFor(createBookCommand => createBookCommand.AuthorIds)
        .NotEmpty();
      RuleFor(createBookCommand => createBookCommand.Year)
        .NotEmpty()
        .InclusiveBetween(0, 9999);
      /* RuleFor(createBookCommand => createBookCommand.GenreIds)
        .NotEmpty(); */
      RuleFor(createBookCommand => createBookCommand.Description)
        .NotEmpty()
        .MaximumLength(1000);
      RuleFor(createBookCommand => createBookCommand.Image)
        .NotEmpty();
      RuleFor(createBookCommand => createBookCommand.BorrowingPeriod)
        .NotEmpty()
        .InclusiveBetween(0, 9999);
      RuleFor(createBookCommand => createBookCommand.Quantity)
        .NotEmpty()
        .InclusiveBetween(0, 9999);
      /* RuleFor(createBookCommand => createBookCommand.BookReviewIds)
        .NotEmpty(); */
    }
  }
}