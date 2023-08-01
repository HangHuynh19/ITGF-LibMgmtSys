using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Contracts.Authors;
using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
  public class AuthorMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<CreateAuthorRequest, CreateAuthorCommand>()
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Biography, src => src.Biography)
        .Map(dest => dest.BookIds, src => src.BookIds);

      config.NewConfig<Author, AuthorResponse>()
        .Map(dest => dest.Id, src => src.Id.Value.ToString())
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Biography, src => src.Biography);
    }
  }
}