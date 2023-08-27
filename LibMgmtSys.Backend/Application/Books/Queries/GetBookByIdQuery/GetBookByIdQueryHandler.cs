using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetBookByIdQuery
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ErrorOr<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetBookByIdAsync(request.Id);
            
            if (book is null)
            {
                return Errors.Book.BookNotFound;
            }
            
            return book;
        }
    }
}

