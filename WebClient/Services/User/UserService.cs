namespace WebClient.Services.User;

/// <summary>
/// Class UserService.
/// Implements the <see cref="WebClient.Services.BaseService{APILibrary.Models.User}" />
/// </summary>
/// <seealso cref="WebClient.Services.BaseService{APILibrary.Models.User}" />
public class UserService: BaseService<APILibrary.Models.User>
{
    /// <summary>
    /// Gets the base path.
    /// </summary>
    /// <value>The base path.</value>
    protected override string BasePath => nameof(User).ToLower() + "s";

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public UserService(HttpClient? httpClient) : base(httpClient)
    {
    }
}