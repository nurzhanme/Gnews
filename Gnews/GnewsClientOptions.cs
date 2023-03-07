namespace Gnews;

public class GnewsClientOptions
{
    public string ApiKey { get; set; }

    public string ApiBaseAddress
    {
        get => string.IsNullOrWhiteSpace(ApiBaseAddress) ? "https://gnews.io/api/v4/" : ApiBaseAddress;
        set => ApiBaseAddress = value;
    }
}