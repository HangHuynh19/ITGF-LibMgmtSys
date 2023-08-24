using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace LibMgmtSys.Backend.Application.Loans.Commands.DeleteBookCommand
{
    public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, ErrorOr<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteLoanCommandHandler(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<Loan>> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.Loan.GetLoanByIdAsync(request.LoanId);
            
            if (loan is null)
            {
                return Errors.Loan.LoanNotFound;
            }
            
            await _unitOfWork.Loan.DeleteLoanAsync(loan);
            return loan;
        }
    }
}

