using AutoMapper;
using ManagementSystem.Application.CQRS.Categories.Commands.Requests;
using ManagementSystem.Application.CQRS.Categories.Commands.Responses;
using ManagementSystem.Application.Security;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Categories.Handlers.CommandHandlers;

public class CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext) : IRequestHandler<CreateCategoryRequest, Result<CreateCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IUserContext _userContext=userContext;

    public async Task<Result<CreateCategoryResponse>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var newCategory = _mapper.Map<Category>(request);
        newCategory.CreatedBy = _userContext.MustGetUserId();

        if (string.IsNullOrEmpty(newCategory.Name))
        {
            throw new BadRequestException("Categoriyanin adi null ve ya bosh ola bilmez");
        }

        await _unitOfWork.CategoryRepository.AddAsync(newCategory);

        var response = _mapper.Map<CreateCategoryResponse>(newCategory);

        return new Result<CreateCategoryResponse>
        {
            Data = response,
            Errors = [],
            IsSuccess = true
        };
    }
}