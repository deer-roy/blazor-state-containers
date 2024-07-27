namespace BlazorState.Pages.MegaContainer;

public record StateMessages(
    bool Loading = false,
    List<ModelMessage> Messages = null,
    string? Error = null
);