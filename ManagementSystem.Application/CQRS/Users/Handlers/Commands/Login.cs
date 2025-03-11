using ManagementSystem.Application.CQRS.Users.DTOs;
using ManagementSystem.Application.Services;
using ManagementSystem.Application.Services.LoggingService;
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
    public class LoginRequest : IRequest<Result<LoginDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IConfiguration configuration, ILoggerService logger) : IRequestHandler<LoginRequest, Result<LoginDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILoggerService _logger = logger;
        public async Task<Result<LoginDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInfo($"Logging attempt  : {request.Email}");

            User user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning($"User does not exist with email: {request.Email}");
                throw new BadRequestException($"Invalid Email : {request.Email}");
            }

            var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

            if (user.PasswordHash != hashedPassword)
            {
                _logger.LogWarning($"Log in Fail due to password does not match");
                throw new BadRequestException("Invalid password");
            }

            if (user != null && hashedPassword == user.PasswordHash)
            {
                List<Claim> authClaim =
                [
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                    new Claim(ClaimTypes.Name , user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role , user.UserRole.ToString())
                ];

                JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                string refreshToken = TokenService.GenerateRefreshToken();

                LoginDto response = new() { AccessToken = tokenString, RefreshToken = refreshToken };

                Domain.Entities.RefreshToken saveRefreshToken = new()
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpirationDate = DateTime.Now.AddDays(double.Parse(configuration.GetSection("JWT:RefreshTokenExpirationDays").Value!))
                };

                await _unitOfWork.RefreshTokenRepository.SaveRefreshToken(saveRefreshToken);
                await _unitOfWork.SaveChange();

                _logger.LogInfo($"User entered website successfully: {request.Email}");
                return new Result<LoginDto> { Data = response };
            }

            _logger.LogWarning($"Log in Fail with any unknown reason");

            return new Result<LoginDto>()
            {
                Data = null,
                Errors = ["Login is failed"]
            };
        }
    }
}