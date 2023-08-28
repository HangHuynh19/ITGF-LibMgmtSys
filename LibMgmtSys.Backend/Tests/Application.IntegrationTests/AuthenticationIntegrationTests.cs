using System.Text;
using LibMgmtSys.Backend.Contracts.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Tests.Application.IntegrationTests
{
    public class IntegrationTests
    {
        private readonly HttpClient _httpClient = new();
        private readonly IConfiguration _configuration;
        
        public IntegrationTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [Fact]
        public async Task Can_Register_User()
        {
            var backendUrl = _configuration["BackendUrl"] + "/api/v1/auth/register";
            var timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var registerRequest = new RegisterRequest("John", "Doe", $"john{timeStamp}@mail.com", "password123");
            var content = new StringContent(
                JsonConvert.SerializeObject(registerRequest),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _httpClient.PostAsync(backendUrl, content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Can_Login_User()
        {
            var backendUrl = _configuration["BackendUrl"] + "/api/v1/auth/login";
            var loginRequest = new LoginRequest("ble@mail.com", "12345678");
            var content = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _httpClient.PostAsync(backendUrl, content);
            
            response.EnsureSuccessStatusCode();
        }
    }
}

