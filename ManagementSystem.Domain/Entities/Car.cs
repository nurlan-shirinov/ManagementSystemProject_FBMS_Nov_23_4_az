namespace ManagementSystem.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Vendor { get; set; }
    public decimal BasePrice { get; set; }
}