using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, ErrorOr<List<Loan>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILoanRepository _loanRepository;
        
        public CreateLoanCommandHandler(
            IBookRepository bookRepository,
            ICustomerRepository customerRepository,
            ILoanRepository loanRepository
        )
        {
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
            _loanRepository = loanRepository;
        }
        
        public async Task<ErrorOr<List<Loan>>> Handle(CreateLoanCommand command, CancellationToken cancellationToken)
        {
            var loans = new List<Loan>();
            var bookIds = command.BookIds.Select(BookId.Create).ToList();
            var books = await _bookRepository.GetBooksByIdsAsync(bookIds);
            var customer = await _customerRepository.GetCustomerByIdAsync(CustomerId.Create(command.CustomerId));
            
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
                    BookId.Create(bookId),
                    customer.Id,
                    DateTime.UtcNow, 
                    DateTime.Now + TimeSpan.FromDays(books[0].BorrowingPeriod)); 
                
                await _loanRepository.AddLoanAsync(loan);
                loans.Add(loan);
            }
            
            return loans;
        }
    }
}

