namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        public IAuthorRepository Author { get; }
        public IBookRepository Book { get; }
        //public IBookReviewRepository BookReview { get; }
        public ICustomerRepository Customer { get; }
        public IGenreRepository Genre { get; }
        public ILoanRepository Loan { get; }
        public IUserRepository User { get; }
        
        int Commit();
        
        Task<int> CommitAsync();
    }
}

