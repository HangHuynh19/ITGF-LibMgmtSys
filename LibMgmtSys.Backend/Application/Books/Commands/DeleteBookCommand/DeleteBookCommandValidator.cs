using FluentValidation;
using FluentValidation.AspNetCore;
using LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand;

namespace LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(deleteBookCommand => deleteBookCommand.BookId)
                .NotEmpty();
        }
    }
}

