using BlazorState.Utils;

namespace BlazorState.Pages.MegaContainer;

public class UseCaseFetchLoginEntries(
    StateContainer<StateLoginEntries> loginEntriesStateContainer
)
{
    public async Task Execute()
    {
        loginEntriesStateContainer.State = loginEntriesStateContainer.State with
        {
            Loading = true,
            Error = null
        };
            
        // delay and return a random value
        await Task.Delay(3000);

        var random = new Random();
        
        // error every now and then
        if (random.Next(1, 10) > 7)
        {
            loginEntriesStateContainer.State = loginEntriesStateContainer.State with
            {
                Loading = false,
                Error = "Things did not end well for that network request."
            };
        }
        
        loginEntriesStateContainer.State = loginEntriesStateContainer.State with
        {
            Loading = false,
            LoginEntries= Enumerable.Range(1, random.Next(19)+ 1)
                .Select(_ => ModelLoginEntry.Random)
                .OrderByDescending(l => l.LoggedInAt)
                .ToList()
        };
        
    }
}