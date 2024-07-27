namespace BlazorState.Pages.MegaContainer;

public record StateProfile(
    bool Loading = false,
    ModelProfile? User = null,
    string? Error = null
);