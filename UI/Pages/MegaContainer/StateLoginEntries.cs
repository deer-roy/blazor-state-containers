namespace BlazorState.Pages.MegaContainer;

public record StateLoginEntries(
    bool Loading = false,
    List<ModelLoginEntry>? LoginEntries = null,
    string? Error = null
);