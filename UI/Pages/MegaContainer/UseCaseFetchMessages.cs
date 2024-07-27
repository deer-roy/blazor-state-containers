using BlazorState.Utils;

namespace BlazorState.UI.Pages.MegaContainer;

public class UseCaseFetchMessages(
    StateContainer<StateMessages> messagesStateContainer
)
{
    public async Task Execute()
    {
        messagesStateContainer.State = messagesStateContainer.State with
        {
            Loading = true,
            Error = null
        };
            
        // delay and return a random value
        await Task.Delay(2000);

        var random = new Random();
        
        // error every now and then
        if (random.Next(1, 10) > 7)
        {
            messagesStateContainer.State = messagesStateContainer.State with
            {
                Loading = false,
                Error = "Things did not end well for that network request."
            };
        }
        
        messagesStateContainer.State = messagesStateContainer.State with
        {
            Loading = false,
            Messages= Enumerable.Range(1, random.Next(3)+ 1)
                .Select(_ => ModelMessage.Random)
                .OrderByDescending(m => m.SentAt)
                .ToList()
        };
        
    }
}