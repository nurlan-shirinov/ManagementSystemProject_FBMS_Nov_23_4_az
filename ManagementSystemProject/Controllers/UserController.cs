using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using static ManagementSystem.Application.CQRS.Users.Handlers.Commands.RefreshToken;
using static ManagementSystem.Application.CQRS.Users.Handlers.Queries.GetByEmail;

namespace ManagementSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{

    private readonly IMediator _mediator = mediator;


    [HttpGet]
    public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
    {
        var request = new Query() { Email = email };
        return Ok(await _mediator.Send(request));
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = new ManagementSystem.Application.CQRS.Users.Handlers.Commands.Delete.Command() { Id = id };
        return Ok(await _mediator.Send(request));
    }


    [HttpPost]
    public async Task<IActionResult> Register([FromBody] ManagementSystem.Application.CQRS.Users.Handlers.Commands.Register.Command request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ManagementSystem.Application.CQRS.Users.Handlers.Commands.Register.Command request)
    {
        return Ok(await _mediator.Send(request));
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] ManagementSystem.Application.CQRS.Users.Handlers.Commands.Login.LoginRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenReuqest request)
    {
        return Ok(await _mediator.Send(request));
    }

}
