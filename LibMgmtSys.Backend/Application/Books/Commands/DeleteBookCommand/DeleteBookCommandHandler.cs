using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using ErrorOr;
using MediatR;

namespace LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ErrorOr<Book>>
    {
        private readonly IBookRepository _bookRepository;
        //private readonly IAuthorRepository _authorRepository;
        //private readonly IGenreRepository _genreRepository;
        
        public DeleteBookCommandHandler(
            IBookRepository bookRepository
            //IAuthorRepository authorRepository,
            //IGenreRepository genreRepository
        )
        {
            _bookRepository = bookRepository;
            //_authorRepository = authorRepository;
            //_genreRepository = genreRepository;
        }
        
        public async Task<ErrorOr<Book>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByIdAsync(request.BookId);
            
            if (book is null)
            {
                return Errors.Book.BookNotFound;
            }
            
            await _bookRepository.DeleteBookAsync(book);
            
            return book;
        }
    }
}

