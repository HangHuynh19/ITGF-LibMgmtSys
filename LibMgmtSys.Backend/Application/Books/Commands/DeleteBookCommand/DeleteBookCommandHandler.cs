using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using ErrorOr;
using MediatR;

namespace LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ErrorOr<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<Book>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Book.GetBookByIdAsync(request.BookId);
            
            if (book is null)
            {
                return Errors.Book.BookNotFound;
            }
            
            await _unitOfWork.Book.DeleteBookAsync(book);
            return book;
        }
    }
}

