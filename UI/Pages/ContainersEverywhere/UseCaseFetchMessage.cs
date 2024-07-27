using BlazorState.Http;
using BlazorState.Utils;

namespace BlazorState.UI.Pages.ContainersEverywhere;

public class UseCaseFetchMessage(
    NetworkClient.NetworkClient networkClient, 
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
        var result = await networkClient.GetMessage();
        var message = result.Data;
        var error = result.Error switch
        {
            HttpConnectionError e => e.Error,
            HttpDeserializationError e => $"Failed to deserialize: {e.Response}.",
            HttpNotOkError e => e.Response,
            _ => null
        };
        messageStateContainer.State = new StateMessage(false, message, error);
    }
}