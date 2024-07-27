using BlazorState.Utils;

namespace BlazorState.UI.Pages.MegaContainer;

public class UseCaseFetchProfile(
    StateContainer<StateProfile> userStateContainer
)
{
    public async Task Execute()
    {
        userStateContainer.State = userStateContainer.State with
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
            userStateContainer.State = userStateContainer.State with
            {
                Loading = false,
                Error = "Things did not end well for that network request."
            };
        }
        
        userStateContainer.State = userStateContainer.State with
        {
            Loading = false,
            User = ModelProfile.Random
        };
        
    }
}