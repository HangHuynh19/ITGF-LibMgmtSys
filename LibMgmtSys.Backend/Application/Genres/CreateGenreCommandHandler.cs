using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Genres
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, ErrorOr<Genre>>
    {
        public readonly IGenreRepository _genreRepository;
        public readonly IBookRepository _bookRepository;
        
        public CreateGenreCommandHandler(IGenreRepository genreRepository, IBookRepository bookRepository)
        {
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
        }
        
        public async Task<ErrorOr<Genre>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = Genre.Create(request.Name);
            var books = await _bookRepository.GetBooksByIdsAsync(request.BookIds);
            
            if (books.Count != request.BookIds.Count)
            {
                return Errors.Book.BookNotFound;
            }
            
            foreach (var book in books)
            {
                genre.AddBook(book);
            }
            
            await _genreRepository.AddGenreAsync(genre);
            return genre;
        }
    }
}

