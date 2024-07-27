using System.Net;

namespace BlazorState.Http2;

public static class HttpNetUtils
{
    public static void EnsureSuccessful(HttpStatusCode statusCode, string response)
    {
        if ((int) statusCode >= 200 && (int) statusCode <= 299) return;
        
        throw new HttpResponseNotOkException(statusCode, response);
    }
    
}