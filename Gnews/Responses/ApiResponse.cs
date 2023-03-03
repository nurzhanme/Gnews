using System.Text.Json.Serialization;

namespace Gnews.Responses;

public class ApiResponse
{
    [JsonPropertyName("totalArticles")]
    public int TotalArticles { get; set; }

    [JsonPropertyName("articles")]
    public List<Article> Articles { get; set; }
}