using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface ILoanRepository
    {
        Task<Loan?> GetLoanByIdAsync(LoanId loanId);
        Task<List<Loan>> GetAllLoansWithPaginationAsync(int pageNumber, int pageSize);
        Task<Loan> AddLoanAsync(Loan loan);
        Task<Loan> DeleteLoanAsync(Loan loan);
    }
}

