using System.Text;
using System.Text.Json;
using Gnews.Constants;
using Gnews.Requests;
using Gnews.Responses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gnews;

public class GnewsClient
{
    private string _apiKey;
    private readonly HttpClient _httpClient;
    private const string TZDFormat = "yyyy-MM-ddTHH:mm:sszzz";

    [ActivatorUtilitiesConstructor]
    public GnewsClient(IOptions<GnewsClientOptions> options, HttpClient httpClient) : this(options.Value, httpClient)
    {
    }

    public GnewsClient(GnewsClientOptions options, HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();

        _httpClient.BaseAddress = new Uri(string.IsNullOrWhiteSpace(options.ApiBaseAddress) ? "https://gnews.io/api/v4/" : options.ApiBaseAddress);

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new ArgumentException(nameof(options.ApiKey));
        }
        _apiKey = options.ApiKey;
    }

    public async Task<ApiResponse> Search(SearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Q))
        {
            throw new ArgumentNullException(nameof(request.Q));
        }

        var queryBuilder = new StringBuilder("search?");
        queryBuilder.Append($"{nameof(request.Q).ToLower()}={request.Q}");

        if (request.Lang.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Lang).ToLower()}={request.Lang.Value.ToString().ToLower()}");
        }

        if (request.Country.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Country).ToLower()}={request.Country.Value.ToString().ToLower()}");
        }

        if (request.Max.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Max).ToLower()}={request.Max.Value}");
        }

        if (request.In.Count > 0)
        {
            queryBuilder.Append($"&{nameof(request.In).ToLower()}={string.Join(',', request.In)}");
        }

        if (request.Nullable.Count > 0)
        {
            queryBuilder.Append($"&{nameof(request.Nullable).ToLower()}={string.Join(',', request.Nullable)}");
        }

        if (request.From.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.From).ToLower()}={request.From.Value.ToString(TZDFormat)}");
        }

        if (request.To.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.To).ToLower()}={request.To.Value.ToString(TZDFormat)}");
        }

        if (request.Sortby.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Sortby).ToLower()}={request.Sortby.Value.ToString().ToLower()}");
        }

        if (request.Page.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Page).ToLower()}={request.Page.Value}");
        }

        if (!string.IsNullOrWhiteSpace(request.Expand))
        {
            queryBuilder.Append($"&{nameof(request.Expand).ToLower()}={request.Expand}");
        }

        var response = await MakeRequest(queryBuilder.ToString()).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ApiResponse>(responseBody);

        return data;
    }

    public async Task<ApiResponse> TopHeadlines(TopHeadlinesRequest request)
    {
        var queryBuilder = new StringBuilder("top-headlines?");
        if (request.Category.HasValue)
        {
            queryBuilder.Append($"{nameof(request.Category).ToLower()}={request.Category.Value.ToString().ToLower()}");
        }
        else
        {
            queryBuilder.Append($"{nameof(request.Category).ToLower()}={Category.General.ToString().ToLower()}");
        }
        
        if (request.Lang.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Lang).ToLower()}={request.Lang.Value.ToString().ToLower()}");
        }

        if (request.Country.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Country).ToLower()}={request.Country.Value.ToString().ToLower()}");
        }

        if (request.Max.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Max).ToLower()}={request.Max.Value}");
        }

        if (request.Nullable.Count > 0)
        {
            queryBuilder.Append($"&{nameof(request.Nullable).ToLower()}={string.Join(',', request.Nullable)}");
        }

        if (request.From.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.From).ToLower()}={request.From.Value.ToString(TZDFormat)}");
        }

        if (request.To.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.To).ToLower()}={request.To.Value.ToString(TZDFormat)}");
        }

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            queryBuilder.Append($"&{nameof(request.Q).ToLower()}={request.Q}");
        }

        if (request.Page.HasValue)
        {
            queryBuilder.Append($"&{nameof(request.Page).ToLower()}={request.Page.Value}");
        }

        if (!string.IsNullOrWhiteSpace(request.Expand))
        {
            queryBuilder.Append($"&{nameof(request.Expand).ToLower()}={request.Expand}");
        }

        var response = await MakeRequest(queryBuilder.ToString()).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ApiResponse>(responseBody);

        return data;
    }

    private Task<HttpResponseMessage> MakeRequest(string requestParams)
    {
        return _httpClient.GetAsync($"{requestParams}&apikey={_apiKey}");
    }
}