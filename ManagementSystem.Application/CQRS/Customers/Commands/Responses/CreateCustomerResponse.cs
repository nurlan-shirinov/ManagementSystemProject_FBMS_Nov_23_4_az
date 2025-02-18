namespace ManagementSystem.Application.CQRS.Customers.Commands.Responses;

public class CreateCustomerResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}