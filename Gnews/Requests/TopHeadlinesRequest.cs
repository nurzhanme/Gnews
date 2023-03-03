using Gnews.Constants;

namespace Gnews.Requests;

public class TopHeadlinesRequest
{
    /// <summary>
    /// This parameter allows you to change the category for the request.
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// This parameter allows you to specify the language of the news articles returned by the API.
    /// You have to set as value the 2 letters code of the language you want to filter.
    /// See the list of <see href="https://gnews.io/docs/v4?csharp#languages">supported languages</see>
    /// </summary>
    public Lang? Lang { get; set; }

    /// <summary>
    /// This parameter allows you to specify the country where the news articles returned by the API were published,
    /// the contents of the articles are not necessarily related to the specified country.
    /// You have to set as value the 2 letters code of the country you want to filter.
    /// See the list of <see href="https://gnews.io/docs/v4?csharp#countries">supported languages</see>
    /// </summary>
    public Country? Country { get; set; }

    /// <summary>
    /// This parameter allows you to specify the number of news articles returned by the API.
    /// The minimum value of this parameter is 1 and the maximum value is 100.
    /// The value you can set depends on your subscription.
    /// See <see href="https://gnews.io/#pricing">the pricing</see> for more information
    /// </summary>
    public int? Max { get; set; }

    /// <summary>
    /// This parameter allows you to specify the attributes that you allow to return null values.
    /// The attributes that can be set are <code>title</code>, <code>description</code> and <code>content</code>.
    /// It is possible to combine several attributes by separating them with a comma.
    /// e.g.<code>title,description</code>
    /// </summary>
    public List<string> Nullable = new();

    /// <summary>
    /// This parameter allows you to filter the articles that have a publication date greater than or equal to the specified value.
    /// The date must respect the following format:
    /// YYYY-MM-DDThh:mm:ssTZD
    /// TZD = time zone designator, its value must always be Z(universal time)
    /// e.g. 2022-08-21T16:27:09Z
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// This parameter allows you to filter the articles that have a publication date greater than or equal to the specified value.
    /// The date must respect the following format:
    /// YYYY-MM-DDThh:mm:ssTZD
    /// TZD = time zone designator, its value must always be Z(universal time)
    /// e.g. 2022-08-21T16:27:09Z
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// This parameter allows you to specify your search keywords which allows you to narrow down the results.
    /// The keywords will be used to return the most relevant articles.
    /// It is possible to use logical operators with keywords,
    /// see the section on <see href="https://gnews.io/docs/v4?csharp#query-syntax">query syntax</see>.
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// This parameter will only work if you have a paid subscription activated on your account.
    /// This parameter allows you to control the pagination of the results returned by the API.
    /// The paging behavior is closely related to the value of the max parameter.
    /// The first page is page 1, then you have to increment by 1 to go to the next page.
    /// Let's say that the value of the max parameter is 10, then the first page will contain the first 10 articles returned by the API (articles 1 to 10),
    /// page 2 will return the next 10 articles (articles 11 to 20), the behavior extends to page 3, 4, ...
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// This parameter will only work if you have a paid subscription activated on your account.
    /// This parameter allows you to return in addition to other data, the full content of the articles.
    /// To get the full content of the articles, the parameter must be set to <code>content</code>
    /// </summary>
    public string? Expand { get; set; }
}