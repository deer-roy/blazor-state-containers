namespace BlazorState.UI.Pages.ContainersEverywhere;

public record StateMessage(
    bool Loading = false,
    string Message = "",
    string? Error = null
);