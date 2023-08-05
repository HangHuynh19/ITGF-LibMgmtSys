using Contracts.Authentication;
using LibMgmtSys.Backend.Application.Authentication.Commands.Register;
using LibMgmtSys.Backend.Application.Authentication.Common;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}

