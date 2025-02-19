using AutoMapper;
using ManagementSystem.Application.CQRS.Categories.Commands.Requests;
using ManagementSystem.Application.CQRS.Categories.Commands.Responses;
using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Application.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCategoryRequest , Category>().ReverseMap();
        CreateMap<Category, CreateCategoryResponse>();
    }
}
