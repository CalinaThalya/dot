public interface IAuthenticationService
{
    Task<bool> AuthenticateUserAsync(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var content = new StringContent(JsonConvert.SerializeObject(new { email, password }), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://api.external-auth.com/authenticate", content);

        return response.IsSuccessStatusCode;
    }
}
