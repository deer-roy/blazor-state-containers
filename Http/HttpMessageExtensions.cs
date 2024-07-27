using System.Text.Json;

namespace BlazorState.Http;

public static class HttpMessageExtensions
{
    
    public static async Task<HttpError?> Unwrap(
        this Task<HttpResponseMessage> task
    )
    {
        try
        {
            var response = await task;
            var responseString = await response.Content.ReadAsStringAsync();
            return !response.IsSuccessStatusCode 
                ? new HttpNotOkError(response.StatusCode, responseString) 
                : null;
        }
        catch (Exception e)
        {
            return new HttpConnectionError(e.Message);
        }
    }
    
    public static async Task<HttpResult<T>> Unwrap<T>(
        this Task<HttpResponseMessage> task
    )
    {
        try
        {
            var response = await task;
            var responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new HttpResult<T>
                {
                    Error = new HttpNotOkError(response.StatusCode, responseString)
                };
            }
            
            var data = JsonSerializer.Deserialize<T>(responseString);
            if (data == null)
            {
                return new HttpResult<T>
                {
                    Error = new HttpDeserializationError(responseString)
                };
            }
            
            return new HttpResult<T>{ Data = data };
        }
        catch (Exception e)
        {
            return new HttpResult<T>
            {
                Error = new HttpConnectionError(e.Message)
            };
        }
    }

}