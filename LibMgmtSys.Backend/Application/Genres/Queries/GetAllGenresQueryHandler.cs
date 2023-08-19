using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Genres.Queries;

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, ErrorOr<List<Genre>>>
{
    private readonly IGenreRepository _genreRepository;
    
    public GetAllGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ErrorOr<List<Genre>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await _genreRepository.GetAllGenresAsync();
    }
}