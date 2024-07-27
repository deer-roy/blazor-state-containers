namespace BlazorState.Utils;

public class StateContainer<T>
{

    private readonly T _initialState;
    private T _state;

    public StateContainer
    (
        T initialState,
        bool autoClear = false
    )
    {
        _initialState = initialState;
        _state = initialState;
        if (autoClear)
        {
            OnChange.OnLastUnsubscribe += Clear;
        }
    }

    public T State
    {
        get => _state;
        set
        {
            _state = value;
            NotifyStateChanged();
        }
    }


    public void Clear()
    {
        _state = _initialState;
    }

    public ActionContainer OnChange = new();

    private void NotifyStateChanged() => OnChange.Invoke();
}

public class ActionContainer()
{
    public Action? OnLastUnsubscribe;

    private int _listenerCount;
    private event Action? OnChange;
    public void Invoke()
    {
        OnChange?.Invoke();
    }

    // Overload the + operator
    public static ActionContainer operator +
    (
        ActionContainer a,
        Action b
    )
    {
        a.OnChange += b;
        a._listenerCount += 1;
        return a;
    }

    public static ActionContainer operator -
    (
        ActionContainer a,
        Action b
    )
    {
        a.OnChange -= b;
        a._listenerCount -= 1;
        Console.WriteLine($"Listeners: {a._listenerCount}");
        if (a._listenerCount == 0)
        {
            a.OnLastUnsubscribe?.Invoke();
        }
        return a;
    }
}