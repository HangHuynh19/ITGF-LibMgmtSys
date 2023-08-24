using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Genres.Commands.CreateGenreCommand
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, ErrorOr<Genre>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateGenreCommandHandler(IUnitOfWork unitOfWork/*, IGenreRepository genreRepository*/)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<Genre>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = Genre.Create(request.Name);
            
            await _unitOfWork.Genre.AddGenreAsync(genre);
            return genre;
        }
    }
}

