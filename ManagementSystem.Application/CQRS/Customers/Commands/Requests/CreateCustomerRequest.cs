using ManagementSystem.Application.CQRS.Customers.Commands.Responses;
using ManagementSystem.Common.GlobalResponses.Generics;
using MediatR;

namespace ManagementSystem.Application.CQRS.Customers.Commands.Requests;

public class CreateCustomerRequest:IRequest<Result<CreateCustomerResponse>>
{
    public string Name { get; set; }
    public string Email { get; set; }
}
