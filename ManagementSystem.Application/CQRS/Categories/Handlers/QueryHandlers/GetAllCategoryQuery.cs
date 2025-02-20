using AutoMapper;
using ManagementSystem.Application.CQRS.Categories.Queries.Requests;
using ManagementSystem.Application.CQRS.Categories.Queries.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Categories.Handlers.QueryHandlers;

public class GetAllCategoryQuery(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCategoryRequest, ResultPagination<GetAllCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultPagination<GetAllCategoryResponse>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
    {
        var categories = _unitOfWork.CategoryRepository.GetAll();
        var totalCount = categories.Count();
        categories = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit);

        return new ResultPagination<GetAllCategoryResponse>
        {
            Data = new Pagination<GetAllCategoryResponse>(
                _mapper.Map<List<GetAllCategoryResponse>>(categories), totalCount, true)
        };
    }
}