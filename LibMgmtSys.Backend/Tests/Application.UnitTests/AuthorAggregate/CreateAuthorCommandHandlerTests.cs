using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.BookAggregate;
using Moq;

namespace Tests.Application.UnitTests.AuthorAggregate
{
    public class CreateAuthorCommandHandlerTests
    {
        private readonly CreateAuthorCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateAuthorCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateAuthorCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_WhenBookIdsAreValid_ShouldReturnAuthor()
        {
            // Arrange
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            _unitOfWorkMock.Setup(uow => uow.Author).Returns(mockAuthorRepository.Object);
            var request = new CreateAuthorCommand("Test Name", "Test Biography");
            
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError is false);
            Assert.Equal("Test Name", result.Value.Name);
            Assert.Equal("Test Biography", result.Value.Biography);
            mockAuthorRepository.Verify(r => r.AddAuthorAsync(It.IsAny<Author>()), Times.Once);
        }
    }
}

