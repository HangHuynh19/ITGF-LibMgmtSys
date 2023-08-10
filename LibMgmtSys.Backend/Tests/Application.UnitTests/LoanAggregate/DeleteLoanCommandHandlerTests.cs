using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Loans.Commands.DeleteBookCommand;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using Moq;
using Tests.Application.UnitTests.BookAggregate;

namespace Tests.Application.UnitTests.LoanAggregate
{
    public class DeleteLoanCommandHandlerTests
    {
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly DeleteLoanCommandHandler _handler;

        public DeleteLoanCommandHandlerTests()
        {
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _handler = new DeleteLoanCommandHandler(_loanRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_WhenLoanNotFound_ReturnsLoanNotFound()
        {
            // Arrange
            var loanId = LoanId.CreateUnique();
            
            _loanRepositoryMock.Setup(x => x.GetLoanByIdAsync(loanId)).ReturnsAsync((Loan)null);
            
            // Act
            var result = await _handler.Handle(new DeleteLoanCommand(loanId), CancellationToken.None);
            
            // Assert
            Assert.True(result.IsError);
            Assert.Equal(Errors.Loan.LoanNotFound, result.Errors[0]);
        }
        
        [Fact]
        public async Task Handle_WhenLoanFound_ReturnsLoan()
        {
            // Arrange
            var loan = Loan.Create(
                BookId.CreateUnique(),
                CustomerId.CreateUnique(),
                DateTime.Now,
                DateTime.Now.AddDays(7));
            
            _loanRepositoryMock.Setup(x => x.GetLoanByIdAsync(loan.Id)).ReturnsAsync(loan);
            
            // Act
            var result = await _handler.Handle(new DeleteLoanCommand(loan.Id), CancellationToken.None);
            
            // Assert
            Assert.False(result.IsError);
            Assert.Equal(loan, result.Value);
            _loanRepositoryMock.Verify(r => r.DeleteLoanAsync(loan), Times.Once);
        }
    }
}