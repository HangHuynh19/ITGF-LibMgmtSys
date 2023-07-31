using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.AuthorAggregate
{
    public sealed class Author : AggregateRoot<AuthorId>
    {
        private readonly List<Book> _books = new();

        public string Name { get; private set; }
        public string Biography { get; private set; }
        public IReadOnlyList<Book> Books => _books.AsReadOnly();

        private Author() : base(AuthorId.CreateUnique())
        {
        }

        private Author(AuthorId authorId, string name, string biography) : base(authorId)
        {
            Name = name;
            Biography = biography;
        }

        public static Author Create(string name, string biography)
        {
            return new Author(AuthorId.CreateUnique(), name, biography);
        }
    }
}