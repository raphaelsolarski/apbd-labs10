using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Context;
using WebApplication1.Dto;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public class UserService(PrescriptionsContext context, IUserRepository userRepository) : IUserService
{
    public const string Issuer = "hardcodedIssuer";
    public const string SecretKey = "sjdflkajsjdfsdkfjskdfjskdfjlskdjfslkdjfjwioeruwoiuerojsdfkllkl";
    public const string Audience = "hardcodedAudience";

    public void RegisterUser(LoginWithPassword loginWithPassword)
    {
        if (userRepository.FindUserByLogin(context, loginWithPassword.Login) != null)
        {
            throw new UserWithGivenLoginAlreadyExistsException();
        }
        var userEntity = new AppUser();
        userEntity.Login = loginWithPassword.Login;
        userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(loginWithPassword.Password);
        userEntity.RefreshToken = GenerateRefreshToken();
        userEntity.RefreshTokenExp = DateTime.Now.AddDays(1);
        userRepository.CreateUser(context, userEntity);
        context.SaveChanges();
    }

    public Tokens Login(LoginWithPassword loginWithPassword)
    {
        var user = userRepository.FindUserByLogin(context, loginWithPassword.Login);
        if (user == null)
        {
            throw new LoginFailedException();
        }

        if (!BCrypt.Net.BCrypt.Verify(loginWithPassword.Password, user.PasswordHash))
        {
            throw new LoginFailedException();
        }

        //probably at this point refresh token could be recreated and saved again to db
        return new Tokens(GenerateAccessToken(), user.RefreshToken);
    }

    public Tokens RefreshToken(string refreshToken)
    {
        var user = userRepository.FindUserByRefreshToken(context, refreshToken);
        if (user == null || user.RefreshTokenExp < DateTime.Now)
        {
            throw new RefreshFailedException();
        }

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        context.SaveChanges();
        return new Tokens(GenerateAccessToken(), user.RefreshToken);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    private string GenerateAccessToken()
    {
        //all the hardcoded values can be extracted to appsettings
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class LoginFailedException() : Exception("Login failed");
    
    public class UserWithGivenLoginAlreadyExistsException() : Exception("User with given login already exists");
    
    public class RefreshFailedException() : Exception("Refresh failed");
}