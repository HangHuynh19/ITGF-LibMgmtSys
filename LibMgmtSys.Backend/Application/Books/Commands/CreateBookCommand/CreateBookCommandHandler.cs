using ErrorOr;
using MediatR;

using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand
{
  public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ErrorOr<Book>>
  {
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateBookCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
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
      
      var authors = await _unitOfWork.Author.GetAuthorsByIdsAsync(command.AuthorIds);
      
      if (authors.Count != command.AuthorIds.Count)
      {
        return Errors.Author.AuthorNotFound;
      }

      foreach (var author in authors)
      {
        book.AddAuthor(author);
      }
        
      var genres = await _unitOfWork.Genre.GetGenresByIdsAsync(command.GenreIds);
        
      if (genres.Count != command.GenreIds.Count)
      {
        return Errors.Genre.GenreNotFound;
      }

      foreach (var genre in genres)
      {
        book.AddGenre(genre);
      }
        
      await _unitOfWork.Book.AddBookAsync(book);
      return book;
    }
  }
}