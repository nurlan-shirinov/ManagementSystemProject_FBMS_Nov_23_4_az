namespace ManagementSystem.Application.CQRS.Users.DTOs;

public  class UpdateDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}