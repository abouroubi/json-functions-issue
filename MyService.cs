using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace Company.Function;
public class MyService
{
    private readonly ILogger<MyService> _log;
    private readonly HttpClient _client;

    public MyService(HttpClient httpClient,
        ILogger<MyService> log)
    {
        _client = httpClient;
        _log = log;
        _client.BaseAddress = new Uri("http://localhost:7071");
    }

    public async Task Login()
    {
        try
        {
            var resourceUrl = "/api/HttpTriggerPost";

            var loginData = new LoginDTO
            {
                Name = "Username",
                Password = "Password"
            };

            _log.LogInformation("Calling PostAsJson");
            var loginResult = await _client.PostAsJsonAsync(resourceUrl, loginData);

            _log.LogInformation("Calling PostAsync");
            var httpContent = new StringContent(loginData.ToJson(), Encoding.UTF8, "application/json");
            var loginResult2 = await _client.PostAsync(resourceUrl, httpContent);

            loginResult.EnsureSuccessStatusCode();
            var stringLoginResult = await loginResult.Content.ReadAsStringAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

