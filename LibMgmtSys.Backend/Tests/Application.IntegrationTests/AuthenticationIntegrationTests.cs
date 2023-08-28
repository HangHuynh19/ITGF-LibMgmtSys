using System.Text;
using LibMgmtSys.Backend.Contracts.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace Tests.Application.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly IConfiguration _configuration;

        public TestFixture ()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.Development.json");
            Configuration = builder.Build();
        }
        
        public IConfiguration Configuration { get; }
        
        public HttpClient HttpClient { get; } = new();

        public void Dispose()
        {
            // Clean up resources if needed
        }
    }

    [CollectionDefinition("IntegrationTests")]
    public class IntegrationTestsCollectionDefinition : ICollectionFixture<TestFixture>
    {
    }
    
    [Collection("IntegrationTests")]
    public class AuthenticationIntegrationTests
    {
        //private readonly HttpClient _httpClient = new();
        private readonly TestFixture _testFixture;
        
        public AuthenticationIntegrationTests(TestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public async Task Can_Register_User()
        {
            var backendUrl = _testFixture.Configuration["BackendUrl"] + "/api/v1/auth/register";
            var timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var registerRequest = new RegisterRequest("John", "Doe", $"john{timeStamp}@mail.com", "password123");
            var content = new StringContent(
                JsonConvert.SerializeObject(registerRequest),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _testFixture.HttpClient.PostAsync(backendUrl, content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Can_Login_User()
        {
            var backendUrl = _testFixture.Configuration["BackendUrl"] + "/api/v1/auth/login";
            var loginRequest = new LoginRequest("ble@mail.com", "12345678");
            var content = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _testFixture.HttpClient.PostAsync(backendUrl, content);

            response.EnsureSuccessStatusCode();
        }
    }
}
