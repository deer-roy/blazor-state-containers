namespace BlazorState.UI.Pages.MegaContainer;

public record StateMessages(
    bool Loading = false,
    bool SendMessagesLoading = false,
    List<ModelMessage>? Messages = null,
    string? Error = null
);