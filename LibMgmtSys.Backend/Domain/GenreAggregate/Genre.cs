using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.GenreAggregate
{
    public sealed class Genre : AggregateRoot<GenreId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Genre(GenreId genreId, string name, string description) : base(genreId)
        {
            Name = name;
            Description = description;
        }
        
        public static Genre Create(string name, string description)
        {
            return new Genre(GenreId.CreateUnique(), name, description);
        }
    }
}