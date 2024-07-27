using BlazorState.Utils;

namespace BlazorState.UI.Pages.MegaContainer;

public class UseCaseSendMessage(
    StateContainer<StateMessages> messagesStateContainer
)
{
    public async Task Execute(ModelMessage message)
    {
        await Task.Delay(1000);
        var messages = new List<ModelMessage>
        {
            message
        };
        if(messagesStateContainer.State.Messages is {} existingMessages)
           messages.AddRange(existingMessages);
       
        messagesStateContainer.State = messagesStateContainer.State with
        {
            Messages = messages
        };
    }
}