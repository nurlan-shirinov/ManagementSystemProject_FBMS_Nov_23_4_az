using ManagementSystem.Application.CQRS.Customers.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController: BaseController
{


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        return Ok(await Sender.Send(request));
    }
}