namespace BlazorState.Pages.Container;

public class NameStateContainer
{
    private string _name = "James Peach";
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}