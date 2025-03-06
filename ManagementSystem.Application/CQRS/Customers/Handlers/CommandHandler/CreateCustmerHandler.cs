using ManagementSystem.Application.CQRS.Customers.Commands.Requests;
using ManagementSystem.Application.CQRS.Customers.Commands.Responses;
using ManagementSystem.Application.Security;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Handlers.CommandHandler;

public class CreateCustmerHandler(IUnitOfWork unitOfWork , IUserContext userContext) : IRequestHandler<CreateCustomerRequest, Result<CreateCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserContext _userContex = userContext;


    public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        Customer newCustomer = new(request.Name, request.Email);
        newCustomer.CreatedBy = _userContex.MustGetUserId();
        await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
        CreateCustomerResponse response = new()
        {
            Id = newCustomer.Id,
            Name = newCustomer.Name,
            Email = newCustomer.Email,
        };

        return new Result<CreateCustomerResponse>()
        {
            Data = response,
            Errors = [],
            IsSuccess = true
        };
    }
}