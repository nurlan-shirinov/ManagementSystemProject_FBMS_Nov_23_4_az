namespace ManagementSystem.Application.CQRS.Users.DTOs;

public class LoginDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}