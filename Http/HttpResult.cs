using System.Text.Json;

namespace BlazorState.Http2;

public abstract class BaseHttpResult
{
    protected readonly Task<HttpResponseMessage> Task;

    protected BaseHttpResult(Task<HttpResponseMessage> task)
    {
        Task = task;
    }

}

public class HttpResult: BaseHttpResult
{
    public HttpResult(Task<HttpResponseMessage> task)
    :base(task)
    {
    }

    public async Task Unwrap()
    {
        var response = await Task;
        var responseString = await response.Content.ReadAsStringAsync();
        HttpNetUtils.EnsureSuccessful(response.StatusCode, responseString);
    }
}

public class HttpDataResult<T>:BaseHttpResult
{
    public HttpDataResult(Task<HttpResponseMessage> task)
    :base(task)
    {
    }

    public async Task<T> Unwrap()
    {
        var response = await Task;
        var responseString = await response.Content.ReadAsStringAsync();
        HttpNetUtils.EnsureSuccessful(response.StatusCode, responseString);

        var data = JsonSerializer.Deserialize<T>(responseString);
        if (data == null)
            throw new HttpResponseDeserializationException(responseString);

        return data;
    }
}