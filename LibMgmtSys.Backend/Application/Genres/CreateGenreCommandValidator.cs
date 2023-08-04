using FluentValidation;

namespace LibMgmtSys.Backend.Application.Genres
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(createGenreCommand => createGenreCommand.Name)
                .NotEmpty()
                .MaximumLength(100);
            /*RuleFor(createGenreCommand => createGenreCommand.BookIds)
                .NotEmpty();*/
        }
    }
}

