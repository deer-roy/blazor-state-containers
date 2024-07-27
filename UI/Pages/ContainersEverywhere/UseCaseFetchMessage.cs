using BlazorState.Http;
using BlazorState.Utils;

namespace BlazorState.UI.Pages.ContainersEverywhere;

public class UseCaseFetchMessage(
    // HttpClient httpClient, 
    StateContainer<StateMessage> messageStateContainer
)
{
    public async Task Execute()
    {
        messageStateContainer.State = messageStateContainer.State with
        {
            Loading = true,
            Error = null
        };
        try
        {
            // An example of how you would make a network call using the unwrap extension
            // similar to what we have in Admin, but improved a little
            // var message = await httpClient.GetAsync("http://localhost:8080/")
            //     .Unwrap<string>();
            
            // for now, we'll just delay and return a random value
            await Task.Delay(2000);

            var random = new Random();
            
            // throw an exception every now and then
            if (random.Next(1, 10) > 7)
            {
                throw new Exception("Things did not end well for that network request");
            }
            var message = $"{RandomNameHelper.GetRandomName()} said something really funny today.";

            messageStateContainer.State = messageStateContainer.State with
            {
                Loading = false,
                Message = message
            };
        }
        catch (HttpResponseNotOkException e)
        {
            messageStateContainer.State = messageStateContainer.State with
            {
                Loading = false,
                Error = e.Response
            };
        }
        catch (Exception e)
        {
            messageStateContainer.State = messageStateContainer.State with
            {
                Loading = false,
                Error = e.Message
            };
            
        }
        
    }
}