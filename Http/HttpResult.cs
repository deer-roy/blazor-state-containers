using System.Net;
using System.Text.Json;

namespace BlazorState.Http;



public abstract record HttpError;

public record HttpConnectionError
(
    string Error
) : HttpError;

public record HttpNotOkError(
    HttpStatusCode StatusCode,
    string Response
) : HttpError
{
    public T? ParseResponse<T>()
    {
        return JsonSerializer.Deserialize<T>(Response);
    }
}

public record HttpDeserializationError(
    string Response
) : HttpError;

public class HttpResult
{
   public HttpError? Error { get; set; }
}
public class HttpResult<T> : HttpResult
{
   public T? Data { get; set; }
}
