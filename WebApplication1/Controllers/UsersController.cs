using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("/api/users")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(LoginWithPassword loginWithPassword)
    {
        try
        {
            userService.RegisterUser(loginWithPassword);
            return Created();
        }
        catch (UserService.UserWithGivenLoginAlreadyExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginWithPassword loginWithPassword)
    {
        try
        {
            return Ok(userService.Login(loginWithPassword));
        }
        catch (UserService.LoginFailedException e)
        {
            return Unauthorized();
        }
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshToken refreshToken)
    {
        try
        {
            var tokens = userService.RefreshToken(refreshToken.refreshToken);
            return Ok(tokens);
        }
        catch (UserService.RefreshFailedException e)
        {
            return BadRequest();
        }
    }

    public class RefreshToken
    {
        [Required] public string refreshToken { get; set; }
    }
}