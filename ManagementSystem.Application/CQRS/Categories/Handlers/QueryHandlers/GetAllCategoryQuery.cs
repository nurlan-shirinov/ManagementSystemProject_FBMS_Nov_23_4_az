using ManagementSystem.Application.CQRS.Categories.Queries.Requests;
using ManagementSystem.Application.CQRS.Categories.Queries.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Categories.Handlers.QueryHandlers;

public class GetAllCategoryQuery(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoryRequest, ResultPagination<GetAllCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResultPagination<GetAllCategoryResponse>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
    {
        var categories = _unitOfWork.CategoryRepository.GetAll();
        var totalCount = categories.Count();
        categories = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        var mappedCategory = new List<GetAllCategoryResponse>();
        foreach (var category in categories)
        {
            var mapped = new GetAllCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                CreatedBy = category.CreatedBy,
                CreatedDate = category.CreatedDate
            };
            mappedCategory.Add(mapped);
        }

        return new ResultPagination<GetAllCategoryResponse>
        {
            Data = new Pagination<GetAllCategoryResponse> { Data = mappedCategory, TotalDataCount = totalCount, IsSuccess = true }
        };
    }
}