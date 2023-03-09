# Gnews

Gnews is a C# wrapper for the GNews API. With Gnews, you can easily integrate the GNews API into your C# application and access news articles and metadata from thousands of news sources worldwide.


If you like this project please give a star and a cup of coffee =)

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/nurzhame)

## Installation

[![NuGet Badge](https://buildstats.info/nuget/Gnews)](https://www.nuget.org/packages/Gnews/)

To install Gnews, you can use the NuGet package manager in Visual Studio. Simply search for "Gnews" and click "Install".

Alternatively, you can install Gnews using the command line:

```
Install-Package Gnews
```

## Getting Started

Obtain valid GNews API key from the https://gnews.io/.

### Without using dependency injection:

```c#
var gnewsClient = new GnewsClient(new GnewsClientOptions()
{
    ApiKey = Environment.GetEnvironmentVariable("MY_GNEWS_API_KEY")
});
```

### Using dependency injection:

In your secrets.json or other settings.json

```json
"GnewsClientOptions": {
  //"ApiKey": "Your api key goes here",
  //"ApiBaseAddress": "If api base has been changed (optional. by default: https://gnews.io/api/v4/)"
},
```

#### Program.cs

```c#
serviceCollection.AddGnewsClient();
```

or using Environment Variable

```c#
serviceCollection.AddGnewsClient(settings => { settings.ApiKey = Environment.GetEnvironmentVariable("MY_GNEWS_API_KEY"); });
```

NOTE: do NOT put your API key directly to your source code.

After injecting your service you will be able to get it from service provider

```c#
var gnewsClient = serviceProvider.GetRequiredService<GnewsClient>();
```

or injecting in the constructor of your class

```c#
public class NewsService
{
    private readonly GnewsClient _gnewsClient;
    
    public NewsService(GnewsClient gnewsClient)
    {
        _gnewsClient = gnewsClient;
    }
}
```

### Search request

```c#
var respone = await gnewsClient.Search(new SearchRequest { Q = "Bitcoin" });

foreach (var article in respone.Articles)
{
    Console.WriteLine(article.Title);
}
```

more details about search request https://gnews.io/docs/v4#search-endpoint
