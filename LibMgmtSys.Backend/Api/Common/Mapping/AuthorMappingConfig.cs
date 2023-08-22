using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Contracts.Authors;
using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
  public class AuthorMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<CreateAuthorRequest, CreateAuthorCommand>()
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Biography, src => src.Biography);

      config.NewConfig<Author, AuthorResponse>()
        .Map(dest => dest.Id, src => src.Id.Value.ToString())
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Biography, src => src.Biography)
        .Map(dest => dest.BookNames, src => src.Books.Select(book => book.Title));

      /*config.NewConfig<Guid, BookId>()
        .Map(dest => dest.Value, src => src);

      config.NewConfig<BookId, Guid>()
        .Map(dest => dest, src => src.Value);*/
    }
  }
}