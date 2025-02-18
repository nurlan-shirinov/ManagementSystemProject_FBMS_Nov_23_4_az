using ManagementSystem.Application.CQRS.Customers.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        return Ok(await _sender.Send(request));
    }
}