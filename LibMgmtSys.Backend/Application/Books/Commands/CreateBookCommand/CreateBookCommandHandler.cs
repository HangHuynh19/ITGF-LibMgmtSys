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
    private readonly IGenreRepository _genreRepository;
    //private readonly IBookReviewRepository _bookReviewRepository;

    public CreateBookCommandHandler(
      IBookRepository bookRepository,
      IAuthorRepository authorRepository,
      IGenreRepository genreRepository
      //IBookReviewRepository bookReviewRepository
    )
    {
      _bookRepository = bookRepository;
      _authorRepository = authorRepository;
      _genreRepository = genreRepository;
      //_bookReviewRepository = bookReviewRepository;
    }

    public async Task<ErrorOr<Book>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
      var book = Book.Create(
        command.Title,
        command.Isbn,
        command.Publisher,
        command.Year,
        command.Description,
        command.BorrowingPeriod,
        command.Quantity,
        command.Image
      );
      var authors = await _authorRepository.GetAuthorsByIdsAsync(command.AuthorIds);
      
      if (authors.Count != command.AuthorIds.Count)
      {
        return Errors.Author.AuthorNotFound;
      }

      foreach (var author in authors)
      {
        book.AddAuthor(author);
      }
      
      var genres = await _genreRepository.GetGenresByIdsAsync(command.GenreIds);
        
      if (genres.Count != command.GenreIds.Count)
      {
        return Errors.Genre.GenreNotFound;
      }

      foreach (var genre in genres)
      {
        book.AddGenre(genre);
      }
      
      await _bookRepository.AddBookAsync(book);
      return book;
    }
  }
}