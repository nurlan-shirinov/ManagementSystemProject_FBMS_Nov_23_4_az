using ManagementSystem.Application.CQRS.Users.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        var request = new Delete.Command() { Id = id };
        return Ok(await _mediator.Send(request));
    }


    [HttpPost]
    public async Task<IActionResult> Register([FromBody] Register.Command request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Update.Command request)
    {
        return Ok(await _mediator.Send(request));
    }

}
