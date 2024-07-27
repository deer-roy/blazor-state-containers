namespace BlazorState.Utils;

public static class StateContainerHelper
{

    static void Subscribe(Action onChange, params StateContainer<object>[] containers)
    {
        foreach (var container in containers)
        {
            container.OnChange += onChange;
        } 
    }

        
    static void Unsubscribe(Action onChange, params StateContainer<object>[] containers)
    {
        foreach (var container in containers)
        {
            container.OnChange -= onChange;
        } 
    }
}