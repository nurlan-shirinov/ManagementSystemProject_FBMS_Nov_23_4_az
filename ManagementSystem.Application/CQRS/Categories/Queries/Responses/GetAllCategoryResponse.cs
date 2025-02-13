namespace ManagementSystem.Application.CQRS.Categories.Queries.Responses;

public class GetAllCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
}
