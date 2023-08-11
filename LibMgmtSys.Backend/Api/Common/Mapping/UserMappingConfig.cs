using LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand;
using LibMgmtSys.Backend.Contracts.Users;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(UpdateUserRequest Request, string Id), UpdateUserCommand>()
                .Map(dest => dest.Id, src => Guid.Parse(src.Id))
                .Map(dest => dest.FirstName, src => src.Request.FirstName)
                .Map(dest => dest.LastName, src => src.Request.LastName)
                .Map(dest => dest.Email, src => src.Request.Email)
                .Map(dest => dest.Password, src => src.Request.Password);

            config.NewConfig<User, UserResponse>();
        }
    }
}

