using BlazorState.Utils;

namespace BlazorState.Pages.MegaContainer;

public class UseCaseSendMessage(
    StateContainer<StateMessages> messagesStateContainer
)
{
    public async Task Execute(ModelMessage message)
    {
        await Task.Delay(1000);
        var messages = messagesStateContainer.State.Messages;
        messages.Insert(0, message);
        messagesStateContainer.State = messagesStateContainer.State with
        {
            Messages = messages
        };
    }
}