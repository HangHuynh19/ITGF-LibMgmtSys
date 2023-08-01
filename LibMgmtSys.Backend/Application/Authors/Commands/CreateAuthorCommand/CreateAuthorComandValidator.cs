using FluentValidation;

namespace LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand
{
  public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
  {
    public CreateAuthorCommandValidator()
    {
      RuleFor(createAuthorCommand => createAuthorCommand.Name)
        .NotEmpty()
        .MaximumLength(100);
      RuleFor(createAuthorCommand => createAuthorCommand.Biography)
        .NotEmpty();
      RuleFor(createAuthorCommand => createAuthorCommand.BookIds)
        .NotEmpty();
    }
  }
}