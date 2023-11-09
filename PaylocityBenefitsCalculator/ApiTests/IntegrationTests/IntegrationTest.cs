using System;
using System.Net.Http;

namespace ApiTests.IntegrationTests;

public class IntegrationTest : IDisposable
{

    //Tried to do it with TestWebApplicationFactoryFactory to created tests with self hosted app but faced issue with duplication calls
    private HttpClient? _httpClient;

    protected HttpClient HttpClient
    {
        get
        {
            if (_httpClient == default)
            {
                _httpClient = new HttpClient
                {
                    //task: update your port if necessary
                    BaseAddress = new Uri("https://localhost:7124")
                };
                _httpClient.DefaultRequestHeaders.Add("accept", "text/plain");
            }

            return _httpClient;
        }
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
