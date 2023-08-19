using Contracts.Genres;
using LibMgmtSys.Backend.Application.Genres.Commands.CreateGenreCommand;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class GenreMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateGenreRequest, CreateGenreCommand>()
                .Map(dest => dest.Name, src => src.Name);

            config.NewConfig<Genre, GenreResponse>()
                .Map(dest => dest.Id, src => src.Id.Value.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.BookNames, src => src.Books.Select(book => book.Title));

            config.NewConfig<Guid, BookId>()
                .Map(dest => dest.Value, src => src);

            config.NewConfig<BookId, Guid>()
                .Map(dest => dest, src => src.Value);
        }
    }
}

