using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Loans.Queries.GetAllLoansQuery;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using Moq;

namespace Tests.Application.UnitTests.LoanAggregate
{
    public class GetAllLoansQueryHandlerTest
    {
        [Fact]
        public async Task GetAllLoansQueryHandler_ShouldReturnAllLoans()
        {
            // Arrange
            var loanRepositoryMock = new Mock<ILoanRepository>();
            var loan1 = Loan.Create(
                BookId.CreateUnique(), 
                CustomerId.CreateUnique(), 
                DateTime.Now,
                DateTime.Now.AddDays(7)
            );
            var loan2 = Loan.Create(
                BookId.CreateUnique(),
                CustomerId.CreateUnique(), 
                DateTime.Now,
                DateTime.Now.AddDays(7),
                DateTime.Now.AddDays(7)
            );
            var loan3 = Loan.Create(
                BookId.CreateUnique(), 
                CustomerId.CreateUnique(), 
                DateTime.Now,
                DateTime.Now.AddDays(7)
            );
            var loans = new List<Loan> { loan1, loan2, loan3 };
            
            loanRepositoryMock.Setup(loanRepository => loanRepository.GetAllLoansWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(loans);
            
            var getAllLoansQuery = new GetAllLoansWithPaginationQuery(PageNumber: 1, PageSize: 3);
            var getAllLoansQueryHandler = new GetAllLoansWithPaginationQueryHandler(
                loanRepositoryMock.Object
            );

            // Act
            var result = await getAllLoansQueryHandler.Handle(getAllLoansQuery, CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Value.Count);
            Assert.Equal(loan1.Id, result.Value[0].Id);
            Assert.Equal(loan2.Id, result.Value[1].Id);
            Assert.Equal(loan3.Id, result.Value[2].Id);
        }
    }
}

