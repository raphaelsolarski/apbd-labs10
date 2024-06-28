namespace WebApplication1.Dto;

public class Tokens(
    string accessToken,
    string refreshToken
)
{
    public string AccessToken { get; } = accessToken;
    public string RefreshToken { get; } = refreshToken;
}