using System.Text;
using LibMgmtSys.Backend.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using Xunit;

namespace Tests.Application.IntegrationTests
{
    public class IntegrationTests
    {
        //private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        
        public IntegrationTests()
        {
            _httpClient = new HttpClient();
        }

        [Fact]
        public async Task Can_Register_User()
        {
            string backendUrl = "http://3.249.217.176/api/v1/auth/register";
            var timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            //var client = _factory.CreateClient();
            var registerRequest = new RegisterRequest("John", "Doe", $"john{timeStamp}@mail.com", "password123");
            var content = new StringContent(
                JsonConvert.SerializeObject(registerRequest),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _httpClient.PostAsync(backendUrl, content);
            //var response = await client.PostAsync("http://3.249.217.176/api/v1/auth/register", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Can_Login_User()
        {
            //var client = _factory.CreateClient();
            string backendUrl = "http://3.249.217.176/api/v1/auth/login";

            var loginRequest = new LoginRequest("ble@mail.com", "12345678");
            var content = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await _httpClient.PostAsync(backendUrl, content);
            //var response = await client.PostAsync("http://3.249.217.176/api/v1/auth/login", content);
            
            response.EnsureSuccessStatusCode();
        }
    }
}

