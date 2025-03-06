using ManagementSystem.Application.CQRS.Categories.Commands.Requests;
using ManagementSystem.Application.CQRS.Categories.Queries.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    [HttpPost]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        return Ok(await Sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryRequest request)
    {
        return Ok(await Sender.Send(request));
    } 
}