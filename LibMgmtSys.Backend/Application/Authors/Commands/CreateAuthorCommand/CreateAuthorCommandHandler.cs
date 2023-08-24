using ErrorOr;
using MediatR;

using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate;

namespace LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand
{
  public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ErrorOr<Author>>
  {
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
      var author = Author.Create(request.Name, request.Biography);
      
      await _unitOfWork.Author.AddAuthorAsync(author);
      return author;
    }
  }
}