using AutoMapper;
using FluentValidation;
using ManagementSystem.Application.CQRS.Users.DTOs;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Common.Security;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Common;
using MediatR;

namespace ManagementSystem.Application.CQRS.Users.Handlers.Commands;

public class Register
{
    public  record struct Command : IRequest<Result<RegisterDto>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }


    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Command> validator) : IRequestHandler<Command, Result<RegisterDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<Command> _validator=validator;


        public async Task<Result<RegisterDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            //var validationResult = await _validator.ValidateAsync(request);
            //if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var isExist = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (isExist != null) throw new BadRequestException("User already registered  with provided email!");

            var newUser = _mapper.Map<User>(request);
            var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
            newUser.PasswordHash = hashPassword;

            await _unitOfWork.UserRepository.RegisterAsync(newUser);

            var response = _mapper.Map<RegisterDto>(newUser);
            return new Result<RegisterDto> { Data = response, Errors = [], IsSuccess = true };
        }
    }
}