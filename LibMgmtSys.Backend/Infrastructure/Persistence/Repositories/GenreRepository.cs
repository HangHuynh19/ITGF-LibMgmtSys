using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibMgmtSys.Backend.Infrastructure.Persistence.Repositories
{
    public sealed class GenreRepository : IGenreRepository
    {
        private readonly LibMgmtSysDbContext _dbContext;
        
        public GenreRepository(LibMgmtSysDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<List<Genre>> GetAllGenresAsync()
        {
            return _dbContext.Genres
                .Include(genre => genre.Books)
                .ToListAsync();
        }
        
        public Task<List<Genre>> GetGenresByIdsAsync(List<GenreId> ids)
        {
            return _dbContext.Genres.Where(genre => ids.Contains(genre.Id)).ToListAsync();
        }
        public Task<Genre?> GetGenreByIdAsync(GenreId id)
        {
            return _dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
        }

        public Task AddGenreAsync(Genre genre)
        {
            _dbContext.Add(genre);
            return _dbContext.SaveChangesAsync();
        }
    }
}

