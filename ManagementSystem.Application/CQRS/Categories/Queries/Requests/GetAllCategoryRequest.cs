using ManagementSystem.Application.CQRS.Categories.Queries.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using MediatR;

namespace ManagementSystem.Application.CQRS.Categories.Queries.Requests;

public class GetAllCategoryRequest : IRequest<ResultPagination<GetAllCategoryResponse>>
{
    public int Limit { get; set; } = 10;
    public int Page { get; set; } = 1;
}