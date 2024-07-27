using Bogus;

namespace BlazorState.UI.Pages.MegaContainer;

public record ModelMessage
(
    DateTime SentAt,
    string From,
    string Message
)
{
    private static readonly Faker<ModelMessage> Generator = new Faker<ModelMessage>()
        .CustomInstantiator(f => new ModelMessage(
            f.Date.Between(DateTime.Now.AddMonths(2), DateTime.Now),
            f.Person.FullName,
            f.Rant.Review(f.Commerce.ProductName())
            ));

    public static ModelMessage Random => Generator.Generate();
}