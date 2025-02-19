namespace ManagementSystem.Application.CQRS.Users.DTOs;

public class GetByEmailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
