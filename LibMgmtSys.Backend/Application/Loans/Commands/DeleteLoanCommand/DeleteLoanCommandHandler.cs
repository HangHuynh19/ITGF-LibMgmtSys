using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Loans.Commands.DeleteBookCommand
{
    public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, ErrorOr<Loan>>
    {
        private readonly ILoanRepository _loanRepository;
        
        public DeleteLoanCommandHandler(
            ILoanRepository loanRepository
        )
        {
            _loanRepository = loanRepository;
        }
        
        public async Task<ErrorOr<Loan>> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            
            if (loan is null)
            {
                return Errors.Loan.LoanNotFound;
            }
            
            await _loanRepository.DeleteLoanAsync(loan);
            
            return loan;
        }
    }
}

