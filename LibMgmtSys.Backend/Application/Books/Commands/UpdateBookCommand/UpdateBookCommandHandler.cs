using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using ErrorOr;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;

namespace LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ErrorOr<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        
        public UpdateBookCommandHandler(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository
        )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<ErrorOr<Book>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByIdAsync(command.Id);
            
            if (book is null)
            {
                return Errors.Book.BookNotFound;
            }

            //var idToBeRemoved = new List<AuthorId>();
            /*foreach (var author in book.Authors)
            {
                if (!command.AuthorIds.Contains(author.Id))
                {
                    //idToBeRemoved.Add(author.Id);
                    book.RemoveAuthor(author);
                }
            }*/

            //var idToBeAdded = new List<AuthorId>();
            /*foreach (var id in command.AuthorIds.Where(id => book.Authors.All(author => author.Id != id)))
            {
                //idToBeAdded.Add(id);
                var author = await _authorRepository.GetAuthorByIdAsync(id);
                if (author is null)
                {
                    return Errors.Author.AuthorNotFound;
                }
                book.AddAuthor(author);
            }*/
            
            book.UpdateBookProperties(
                command.Title,
                command.Isbn,
                command.Publisher,
                command.Year,
                command.Description,
                command.Image,
                command.BorrowingPeriod,
                command.Quantity
            );

            await _bookRepository.UpdateBookAsync(book);
            
            return book;
        }
    }
}