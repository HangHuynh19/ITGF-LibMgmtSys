using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
  public class AuthorRepository : IAuthorRepository
  {
    private readonly LibMgmtSysDbContext _dbContext;
    //private readonly IBookRepository _bookRepository;

    public AuthorRepository(LibMgmtSysDbContext dbContext)
    {
      _dbContext = dbContext;
      //_bookRepository = bookRepository;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
      return await _dbContext.Authors
        .Include(author => author.Books)
        .ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(AuthorId id)
    {
      return await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<List<Author>> GetAuthorsByIdsAsync(List<AuthorId> ids)
    {
      return await _dbContext.Authors.Where(author => ids.Contains(author.Id)).ToListAsync();
    }

    public async Task AddAuthorAsync(Author author)
    {
      _dbContext.Add(author);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
      _dbContext.Update(author);
      await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(Author author)
    {
      _dbContext.Remove(author);
      await _dbContext.SaveChangesAsync();
    }
  }
}