using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery
{
    public class GetAllBooksWithPaginationQueryHandler : IRequestHandler<GetAllBooksWithPaginationQuery, ErrorOr<List<Book>>>
    {
        private readonly IUnitOfWork _unitOfWork;
    
        public GetAllBooksWithPaginationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        public async Task<ErrorOr<List<Book>>> Handle(GetAllBooksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Book.GetAllBooksWithPaginationAsync(
                request.PageNumber, 
                request.PageSize, 
                request.SortOrder, 
                request.SearchTerm.ToLower().Trim());
        }
    }
}

