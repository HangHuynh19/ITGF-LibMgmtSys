using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, ErrorOr<List<Loan>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateLoanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Loan>>> Handle(CreateLoanCommand command, CancellationToken cancellationToken)
        {
            var loans = new List<Loan>();
            var bookIds = command.BookIds.Select(bookId => BookId.Create(Guid.Parse(bookId))).ToList();
            
            var books = await _unitOfWork.Book.GetBooksByIdsAsync(bookIds);
            var customer = await _unitOfWork.Customer.GetCustomerByUserIdAsync(UserId.Create(command.CustomerId));
            
            if (books.Count != command.BookIds.Count)
            {
                return Errors.Book.BookNotFound;
            }

            if (customer is null)
            {
                return Errors.Customer.CustomerNotFound;
            }

            foreach (var bookId in command.BookIds)
            {
                var loan = Loan.Create(
                    BookId.Create(Guid.Parse(bookId)),
                    customer.Id,
                    DateTime.UtcNow,
                    DateTime.Now + TimeSpan.FromDays(books[0].BorrowingPeriod));
                
                await _unitOfWork.Loan.AddLoanAsync(loan);
                loans.Add(loan);
            }

            return loans;
        }
    }
}

