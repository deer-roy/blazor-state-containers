using System.Text.Json;

namespace BlazorState.Http;

public static class HttpMessageExtensions
{

    #region Only setup the network calls to be awaited at a later stage
    
    /**
     * Use these methods when you are not ready to catch exceptions.
     * This allows you to decouple making the network requests with
     * your error handling.
     *
     * e.g. Say you wanted to dispatch exceptions from a network
     *      call using Fluxor. Then without this, you would have
     *      to couple your network service with a Fluxor dispatcher,
     *      so that you can dispatch the exceptions. This could be
     *      by having a parameter in the method or as a
     *      property of your network service.
     *      
     *          public async Task<string> GetName(Dispatcher d){
     //            //naturally, you may want to extract the boiler plate elsewhere
     //            //but you will still need your Dispatcher in there somewhere
     *              try {
     *                  var response = await _httpClient.GetAsync(...);
     *                  if(!response.IsSuccessStatusCode)
     *                      return await response.Content.ReadAsStringAsync();
     *                  throw new NetworkFailedException(...);
     *              } catch(Exception e) {
     *                  d.Dispatch(e) 
     *              }
     *          }
     *
     *      but with DataResult, you can keep your network service
     *      free from Fluxor or anything else you might need when you
     *      are ready to catch the exceptions. Your method would look
     *      like this.
     *      
     *          public HttpDataResult<string> GetName(){
     *              return _httpClient.GetAsync(...).AsDataResult<string>();
     *          }
     *
     *      Then in your effect, where you have all you need
     * 
     *          public Task SomeEffect(Dispatcher d){
     *              try {
     *                  var name = await _networkService.GetName();
     //                // use the name ... 
     *               } catch(Exception e) {
     *                  d.Dispatch(e) 
     *              }
     *          }
     *
     */
    public static HttpDataResult<T> AsDataResult<T>(
        this Task<HttpResponseMessage> task
    )
    {
        return new HttpDataResult<T>(task);
    }

    public static HttpResult AsResult(
        this Task<HttpResponseMessage> task
    )
    {
        return new HttpResult(task);
    }
    #endregion

    #region Immediately process network request
    
    /**
     * Use these methods when you are ready to catch the exceptions.
     */
    public static async Task<T> Unwrap<T>(
        this Task<HttpResponseMessage> task
    )
    {
        var response = await task;
        var responseString = await response.Content.ReadAsStringAsync();
        HttpNetUtils.EnsureSuccessful(response.StatusCode, responseString);

        var data = JsonSerializer.Deserialize<T>(responseString);
        if (data == null)
            throw new HttpResponseDeserializationException(responseString);

        return data;
    }

    public static async Task Unwrap(
        this Task<HttpResponseMessage> task
    )
    {
        var response = await task;
        var responseString = await response.Content.ReadAsStringAsync();
        HttpNetUtils.EnsureSuccessful(response.StatusCode, responseString);
    }
    
    #endregion
}