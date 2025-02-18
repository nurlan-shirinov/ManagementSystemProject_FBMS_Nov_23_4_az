using ManagementSystem.Domain.BaseEntities;

namespace ManagementSystem.Domain.Entities;

public class Customer : BaseEntity
{
    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; set; }
    public string Email { get; set; }
}
