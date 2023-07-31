using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.GenreAggregate
{
    public sealed class Genre : AggregateRoot<GenreId>
    {
        private readonly List<Book> _books = new();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        private Genre() : base(GenreId.CreateUnique())
        {
        }

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