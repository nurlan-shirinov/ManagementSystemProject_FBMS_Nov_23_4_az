using ManagementSystem.Application.CQRS.Users.DTOs;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Users.Handlers.Queries;

public class GetByEmail
{
    public record struct Query : IRequest<Result<GetByEmailDto>>
    {
        public string Email { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<GetByEmailDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByEmailDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);

            if (currentUser == null)
            {
                throw new BadRequestException("User is not exist with provided email");
            }

            var response = new GetByEmailDto()
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                Surname = currentUser.Surname,
                Email = currentUser.Email,
                Phone = currentUser.Phone,
            };
            return new Result<GetByEmailDto> { Data = response, Errors = [], IsSuccess = true };

        }
    }
}