using System.Text.Json.Serialization;

namespace BlazorState.Http2;

public class ProblemDetails
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int? Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }
}

public class ValidationProblemDetails: ProblemDetails
{
    [JsonPropertyName("errors")] 
    public IDictionary<string, List<string>> Errors { get; } = 
        new Dictionary<string, List<string>>(StringComparer.Ordinal);
}