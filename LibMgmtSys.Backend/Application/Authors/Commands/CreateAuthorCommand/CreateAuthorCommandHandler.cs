using ErrorOr;
using MediatR;

using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

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
      var books = await _bookRepository.GetBooksByIdsAsync(request.BookIds);
      List<Error> errors = new();
      if (books.Count != request.BookIds.Count)
      {
        return Errors.Book.BookNotFound;
      }
      
      foreach (var book in books)
      {
        author.AddBook(book);
      }
      
      await _authorRepository.AddAuthorAsync(author);
      return author;
    }
  }
}