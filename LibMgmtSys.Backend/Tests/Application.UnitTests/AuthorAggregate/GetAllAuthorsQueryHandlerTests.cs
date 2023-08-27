using LibMgmtSys.Backend.Application.Authors.Queries;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using Moq;

namespace Tests.Application.UnitTests.AuthorAggregate
{
    public class GetAllAuthorsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetAllAuthorsQueryHandler _getAllAuthorsQueryHandler;
        
        public GetAllAuthorsQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _getAllAuthorsQueryHandler = new GetAllAuthorsQueryHandler(_unitOfWorkMock.Object);
        }
        
        [Fact]
        public async Task Handle_ShouldReturnListOfAuthors_WhenRequestIsValid()
        {
            // Arrange
            var request = new GetAllAuthorsQuery();
            var expectedAuthors = new List<Author>
            {
                Author.Create("Author 1", "Biography 1"),
                Author.Create("Author 2", "Biography 2"),
                Author.Create("Author 3", "Biography 3"),
            };
            
            _unitOfWorkMock
                .Setup(unitOfWork => unitOfWork.Author.GetAllAuthorsAsync())
                .ReturnsAsync(expectedAuthors);
            
            // Act
            var result = await _getAllAuthorsQueryHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.Equal(3, result.Value.Count);
            Assert.Equal(expectedAuthors, result.Value);
        }
    }
}