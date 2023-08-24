using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetBooksByIdsQuery
{
    public class GetBooksByIdsQueryHandler : IRequestHandler<GetBooksByIdsQuery, ErrorOr<List<Book>>>
    {
        private readonly IUnitOfWork _unitOfWork;
    
        public GetBooksByIdsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        public async Task<ErrorOr<List<Book>>> Handle(GetBooksByIdsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Book.GetBooksByIdsAsync(request.BookIds);
        }
    }
}

