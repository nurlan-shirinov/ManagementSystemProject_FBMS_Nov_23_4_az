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

}
