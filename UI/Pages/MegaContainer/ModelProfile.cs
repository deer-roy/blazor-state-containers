using Bogus;

namespace BlazorState.Pages.MegaContainer;

public record ModelProfile
(
    string UserName,
    string Email,
    string ProfileImageUrl
)
{

    private static readonly Faker<ModelProfile> Generator = new Faker<ModelProfile>()
        .CustomInstantiator(f => new ModelProfile(
            f.Internet.UserName(),
            f.Internet.Email(),
            f.Internet.Avatar()));

    public static ModelProfile Random => Generator.Generate();
}
