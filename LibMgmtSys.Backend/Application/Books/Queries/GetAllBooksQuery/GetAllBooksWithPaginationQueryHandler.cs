using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery;

public class GetAllBooksWithPaginationQueryHandler : IRequestHandler<GetAllBooksWithPaginationQuery, ErrorOr<List<Book>>>
{
    private readonly IBookRepository _bookRepository;
    
    public GetAllBooksWithPaginationQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<ErrorOr<List<Book>>> Handle(GetAllBooksWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetAllBooksWithPaginationAsync(
            request.PageNumber, 
            request.PageSize, 
            request.SortOrder, 
            request.SearchTerm.ToLower().Trim());
    }
}