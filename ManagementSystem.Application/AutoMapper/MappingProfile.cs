using AutoMapper;
using ManagementSystem.Application.CQRS.Categories.Commands.Requests;
using ManagementSystem.Application.CQRS.Categories.Commands.Responses;
using ManagementSystem.Application.CQRS.Categories.Queries.Responses;
using ManagementSystem.Application.CQRS.Users.DTOs;
using ManagementSystem.Domain.Entities;
using static ManagementSystem.Application.CQRS.Users.Handlers.Commands.Register;

namespace ManagementSystem.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User
        CreateMap<Command, User>();
        CreateMap<User, RegisterDto>();
        
        #endregion


        #region Category
        CreateMap<CreateCategoryRequest, Category>().ReverseMap();
        CreateMap<Category, CreateCategoryResponse>();
        CreateMap<Category, GetAllCategoryResponse>();
        #endregion
    }
}