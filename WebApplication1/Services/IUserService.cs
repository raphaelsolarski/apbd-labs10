using WebApplication1.Controllers;
using WebApplication1.Dto;

namespace WebApplication1.Services;

public interface IUserService
{
    void RegisterUser(LoginWithPassword loginWithPassword);

    Tokens Login(LoginWithPassword loginWithPassword);
    Tokens RefreshToken(string refreshToken);
}