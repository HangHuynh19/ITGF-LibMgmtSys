using LibMgmtSys.Backend.Domain.AuthorAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;

namespace LibMgmtSys.Backend.Application.Authors.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, ErrorOr<List<Author>>>
    {
        private readonly IUnitOfWork _unitOfWork;
    
        public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Author.GetAllAuthorsAsync();
        }
    }
}

