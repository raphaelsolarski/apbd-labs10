using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto;

public class LoginWithPassword
{
    [Required] public string Login { get; set; }
    [Required] public string Password { get; set; }
}