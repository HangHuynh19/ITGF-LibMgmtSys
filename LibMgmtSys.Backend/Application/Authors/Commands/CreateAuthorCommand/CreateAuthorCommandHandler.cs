using ErrorOr;
using MediatR;

using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand
{
  public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ErrorOr<Author>>
  {
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
      _authorRepository = authorRepository;
      _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
      var author = Author.Create(request.Name, request.Biography);
      
      foreach (var bookId in request.BookIds)
      {
        var book = await _bookRepository.GetBookByIdAsync(bookId);

        if (book is null)
        {
          return Errors.Author.BookNotFound;
        }

        author.AddBook(book);
      }

      await _authorRepository.AddAuthorAsync(author);
      return author;
    }
  }
}