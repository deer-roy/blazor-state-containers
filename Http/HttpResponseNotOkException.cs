using System.Net;
using System.Text.Json;

namespace BlazorState.Http2;

public class HttpResponseNotOkException
(
    HttpStatusCode statusCode,
    string response
) : Exception($"Http response status not successful: [Status {(int) statusCode}] {response}")
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string Response { get; } = response;
    
    public T? ParseResponse<T>()
    {
        return JsonSerializer.Deserialize<T>(Response); 
    }
}

public class HttpResponseDeserializationException(string response) : 
    Exception($"Failed to deserialize http response: {response}");