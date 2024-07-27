using BlazorState.Http;

namespace BlazorState.NetworkClient;

public class NetworkClient(HttpClient httpClient)
{

    public Task<HttpResult<string>> GetMessage() =>
        httpClient
            .GetAsync("http://localhost:8080/")
            .Unwrap<string>();
}