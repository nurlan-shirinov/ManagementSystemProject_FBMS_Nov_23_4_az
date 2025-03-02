﻿using ManagementSystem.Application.Services;
using ManagementSystem.Common.Exceptions;
using ManagementSystem.Common.GlobalResponses.Generics;
using ManagementSystem.Common.Security;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ManagementSystem.Application.CQRS.Users.Handlers.Commands;

public class Login
{
    public class LoginRequest : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IConfiguration configuration) : IRequestHandler<LoginRequest, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        public async Task<Result<string>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email) ?? throw new BadRequestException($"Invalid Email : {request.Email}");
            var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

            if (user.PasswordHash != hashedPassword)
            {
                throw new BadRequestException("Invalid password");
            }

            if (user != null && hashedPassword == user.PasswordHash)
            {
                List<Claim> authClaim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                    new Claim(ClaimTypes.Name , user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role , user.UserRole.ToString())
                };

                JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new Result<string> { Data = tokenString };

            }

            return new Result<string>()
            {
                Data = "Something went wrong"
            };
        }
    }
}