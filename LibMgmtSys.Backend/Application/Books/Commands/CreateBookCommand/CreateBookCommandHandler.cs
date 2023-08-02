using ErrorOr;
using MediatR;

using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand
{
  public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ErrorOr<Book>>
  {
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    //private readonly IGenreRepository _genreRepository;
    //private readonly IBookReviewRepository _bookReviewRepository;

    public CreateBookCommandHandler(
      IBookRepository bookRepository,
      IAuthorRepository authorRepository
      //IGenreRepository genreRepository,
      //IBookReviewRepository bookReviewRepository
    )
    {
      _bookRepository = bookRepository;
      _authorRepository = authorRepository;
      //_genreRepository = genreRepository;
      //_bookReviewRepository = bookReviewRepository;
    }

    public async Task<ErrorOr<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
      var book = Book.Create(
        request.Title,
        request.Isbn,
        request.Publisher,
        request.Year,
        request.Description,
        request.Image,
        request.BorrowingPeriod,
        request.Quantity
      );

      /* foreach (var authorId in request.AuthorIds)
      {
        var author = await _authorRepository.GetAuthorByIdAsync(authorId);

        if (author is null)
        {
          return Errors.Book.AuthorNotFound;
        }

        book.AddAuthor(author);
      } */

      await _bookRepository.AddBookAsync(book);
      return book;
    }
  }
}