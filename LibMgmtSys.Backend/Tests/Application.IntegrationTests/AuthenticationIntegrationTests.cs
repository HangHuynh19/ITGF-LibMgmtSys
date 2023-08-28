using System.Text;
using LibMgmtSys.Backend.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests.Application.IntegrationTests
{
    public class AuthenticationIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        
        public AuthenticationIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Can_Register_User()
        {
            var timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var client = _factory.CreateClient();
            var registerRequest = new RegisterRequest("John", "Doe", $"john{timeStamp}@mail.com", "password123");
            var content = new StringContent(
                JsonConvert.SerializeObject(registerRequest),
                Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync("/api/v1/auth/register", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Can_Login_User()
        {
            var client = _factory.CreateClient();
            var loginRequest = new LoginRequest("thanh@mail.com", "12345678");
            var content = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync("/api/v1/auth/login", content);
            
            response.EnsureSuccessStatusCode();
        }
    }
}

