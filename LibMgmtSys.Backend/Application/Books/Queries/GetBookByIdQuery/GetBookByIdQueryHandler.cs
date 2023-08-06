using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetBookByIdQuery
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ErrorOr<Book>>
    {
        private readonly IBookRepository _bookRepository;
    
        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task<ErrorOr<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByIdAsync(request.Id);
            
            if (book is null)
            {
                return Errors.Book.BookNotFound;
            }
            
            return book;
        }
    }
}

