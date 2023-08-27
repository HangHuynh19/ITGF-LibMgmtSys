using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Genres.Queries;

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, ErrorOr<List<Genre>>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllGenresQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<Genre>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Genre.GetAllGenresAsync();
    }
}