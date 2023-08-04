using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.GenreAggregate
{
    public sealed class Genre : AggregateRoot<GenreId>
    {
        private readonly List<Book> _books = new();
        public string Name { get; private set; }
        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        private Genre() : base(GenreId.CreateUnique())
        {
        }

        private Genre(GenreId genreId, string name) : base(genreId)
        {
            Name = name;
        }
        
        public static Genre Create(string name)
        {
            return new Genre(GenreId.CreateUnique(), name);
        }
        
        public void AddBook(Book book)
        {
            _books.Add(book);
        }
    }
}