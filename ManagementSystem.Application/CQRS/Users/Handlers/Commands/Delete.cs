using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Users.Handlers.Commands;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            _unitOfWork.UserRepository.Remove(request.Id);
            return new Result<Unit> { Errors=[] , IsSuccess=true };
        }
    }
}