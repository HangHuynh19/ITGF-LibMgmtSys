using ErrorOr;
using MediatR;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ErrorOr<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Book>> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetBookByIdAsync(BookId.Create(Guid.Parse(command.Id)));
            
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
                command.Image!,
                command.BorrowingPeriod,
                command.Quantity
            );
            
            await _unitOfWork.Book.UpdateBookAsync(book);
            return book;
        }
    }
}
