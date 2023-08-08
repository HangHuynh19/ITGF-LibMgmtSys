using ErrorOr;

namespace LibMgmtSys.Backend.Domain.Common.DomainErrors
{
    public static partial class Errors
    {
        public static class Genre
        {
            public static Error GenreNotFound = Error.Validation(
                code: "Genre.InvalidGenreId",
                description: "Genre with given ID does not exist"
            );
        }
    }
}
