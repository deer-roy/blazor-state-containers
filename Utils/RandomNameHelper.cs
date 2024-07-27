namespace BlazorState.Utils;

public static class RandomNameHelper
{
    private static readonly List<string> Names =
    [
        "Emma Thompson",
        "Liam Rodriguez",
        "Olivia Chen",
        "Noah Patel",
        "Ava Nguyen",
        "Ethan Kim",
        "Sophia Martinez",
        "Mason Jackson",
        "Isabella Singh",
        "William Lee",
        "Mia Gupta",
        "James Wilson",
        "Charlotte Brown",
        "Benjamin Davis",
        "Amelia Johnson",
        "Elijah Taylor",
        "Harper Anderson",
        "Lucas White",
        "Evelyn Garcia",
        "Alexander Moore"
    ];

    private static readonly Random Random = new();

    public static string GetRandomName()
    {
        var index = Random.Next(Names.Count);
        return Names[index];
    }
}