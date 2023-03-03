using System.Text.Json.Serialization;

namespace Gnews.Responses;

public class Source
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}