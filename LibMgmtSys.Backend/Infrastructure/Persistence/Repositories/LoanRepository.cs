using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibMgmtSysDbContext _dbContext;
        
        public LoanRepository(LibMgmtSysDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Loan?> GetLoanByIdAsync(LoanId loanId)
        {
            return await _dbContext.Loans.FirstOrDefaultAsync(loan => loan.Id.Equals(loanId));
        }

        public Task<List<Loan>> GetAllLoansWithPaginationAsync(int pageNumber, int pageSize)
        {
            return _dbContext.Loans
                .Include(book => book.Book)
                .Include(customer => customer.Customer)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Loan> AddLoanAsync(Loan loan)
        {
            _dbContext.Loans.Add(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> DeleteLoanAsync(Loan loan)
        {
            _dbContext.Remove(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }
    }
}

