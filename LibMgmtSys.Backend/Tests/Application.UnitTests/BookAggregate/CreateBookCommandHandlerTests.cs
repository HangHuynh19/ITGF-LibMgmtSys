using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using Moq;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class CreateBookCommandHandlerTests
    {
        private readonly CreateBookCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateBookCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateBookCommandHandler(
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_EverythingIsValid_ShouldReturnBook()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            _unitOfWorkMock.Setup(uow => uow.Book).Returns(mockBookRepository.Object);
            
            var authorIds = new List<AuthorId>
            {
                AuthorId.CreateUnique(), 
                AuthorId.CreateUnique()
            };
            var genreIds = new List<GenreId>
            {
                GenreId.CreateUnique(), 
                GenreId.CreateUnique()
            };
            var request = new CreateBookCommand(
                "Title", 
                "Description", 
                "Publisher", 
                authorIds, 
                1998, 
                genreIds,
                "Description", 
                new Uri("https://www.google.com/"), 
                14, 
                1);
            var authors = new List<Author>
            {
                Author.Create("Test Name1", "Test Biography1"),
                Author.Create("Test Name2", "Test Biography2")
            };
            var genres = new List<Genre> { Genre.Create("Test Genre1"), Genre.Create("Test Genre2") };
            
            _unitOfWorkMock.Setup(r => r.Author.GetAuthorsByIdsAsync(authorIds))
                .ReturnsAsync(authors);
            _unitOfWorkMock.Setup(r => r.Genre.GetGenresByIdsAsync(genreIds))
                .ReturnsAsync(genres);
            
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError is false);
            Assert.Equal("Title", result.Value.Title);
            Assert.Equal("Description", result.Value.Description);
            Assert.Equal("Publisher", result.Value.Publisher);
            Assert.Equal(1998, result.Value.Year);
            Assert.Equal("Description", result.Value.Description);
            Assert.Equal(new Uri("https://www.google.com/"), result.Value.Image);
            Assert.Equal(2, result.Value.Authors.Count);
            Assert.Collection(result.Value.Authors,
                author => Assert.Equal("Test Name1", author.Name),
                author => Assert.Equal("Test Name2", author.Name));
        }

        [Fact]
        public async Task Handle_WhenAuthorIdsAreInvalid_ShouldReturnAuthorNotFound()
        {
            var command = new CreateBookCommand(
                "Title",
                "Description",
                "Publisher",
                new List<AuthorId> { AuthorId.CreateUnique(), AuthorId.CreateUnique() },
                1998,
                new List<GenreId> { GenreId.CreateUnique() },
                "Description",
                new Uri("https://www.google.com/"),
                14,
                1);
            var foundAuthor = Author.Create(
                "Test Name",
                "Test Biography");
            
            _unitOfWorkMock.Setup(r => r.Author.GetAuthorsByIdsAsync(command.AuthorIds))
                .ReturnsAsync(new List<Author> { foundAuthor });
            _unitOfWorkMock.Setup(r => r.Genre.GetGenresByIdsAsync(command.GenreIds))
                .ReturnsAsync(new List<Genre> { Genre.Create("Test Genre") });
            
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(Errors.Author.AuthorNotFound, result.Errors[0]);
        }
        
        [Fact]
        public async Task Handle_WhenGenreIdsAreInvalid_ShouldReturnGenreNotFound()
        {
            var command = new CreateBookCommand(
                "Title",
                "Description",
                "Publisher",
                new List<AuthorId> { AuthorId.CreateUnique() },
                1998,
                new List<GenreId> { GenreId.CreateUnique(), GenreId.CreateUnique() },
                "Description",
                new Uri("https://www.google.com/"),
                14,
                1);
            var foundGenre = Genre.Create("Test Genre");
            
            _unitOfWorkMock.Setup(r => r.Author.GetAuthorsByIdsAsync(command.AuthorIds))
                .ReturnsAsync(new List<Author> { Author.Create("Test Name", "Test Biography") });
            _unitOfWorkMock.Setup(r => r.Genre.GetGenresByIdsAsync(command.GenreIds))
                .ReturnsAsync(new List<Genre> { foundGenre });
            
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(Errors.Genre.GenreNotFound, result.Errors[0]);
        }
    }
}

