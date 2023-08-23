using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibMgmtSysDbContext _dbContext;
        private IAuthorRepository _author;
        private IBookRepository _book;
        //private IBookReviewRepository _bookReview;
        private ICustomerRepository _customer;
        private IGenreRepository _genre;
        private ILoanRepository _loan;
        private IUserRepository _user;
        
        public UnitOfWork(LibMgmtSysDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IAuthorRepository Author
        {
            get
            {
                if (_author == null)
                {
                    _author = new AuthorRepository(_dbContext);
                }

                return _author;
            }
        }
        
        public IBookRepository Book
        {
            get
            {
                if (_book == null)
                {
                    _book = new BookRepository(_dbContext);
                }

                return _book;
            }
        }
        
        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_dbContext);
                }

                return _customer;
            }
        }
        
        public IGenreRepository Genre
        {
            get
            {
                if (_genre == null)
                {
                    _genre = new GenreRepository(_dbContext);
                }

                return _genre;
            }
        }
        
        public ILoanRepository Loan
        {
            get
            {
                if (_loan == null)
                {
                    _loan = new LoanRepository(_dbContext);
                }

                return _loan;
            }
        }
        
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_dbContext);
                }

                return _user;
            }
        }
        
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

