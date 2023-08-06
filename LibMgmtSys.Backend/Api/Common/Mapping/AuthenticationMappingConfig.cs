using Contracts.Authentication;
using LibMgmtSys.Backend.Application.Authentication.Commands.Register;
using LibMgmtSys.Backend.Application.Authentication.Common;
using LibMgmtSys.Backend.Application.Authentication.Queries.Login;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}

