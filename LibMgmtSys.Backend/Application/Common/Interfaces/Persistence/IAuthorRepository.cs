using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.AuthorAggregate;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Persistence
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(AuthorId id);
        Task<List<Author>> GetAuthorsByIdsAsync(List<AuthorId> ids);
        Task AddAuthorAsync(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}