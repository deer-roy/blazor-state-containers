using Bogus;

namespace BlazorState.UI.Pages.MegaContainer;

public record ModelLoginEntry
(
    DateTime LoggedInAt,
    string UserAgent,
    string Location
)
{
    private static readonly Faker<ModelLoginEntry> Generator = new Faker<ModelLoginEntry>()
        .CustomInstantiator(f => new ModelLoginEntry(
            f.Date.Between(DateTime.Now.AddMonths(1), DateTime.Now),
            f.Internet.UserAgent(),
            f.Address.Country()
        ));

    public static ModelLoginEntry Random => Generator.Generate();
}