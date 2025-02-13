﻿using ManagementSystem.Application.CQRS.Categories.Commands.Requests;
using ManagementSystem.Application.CQRS.Categories.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryRequest request)
    {
        return Ok(await _sender.Send(request));
    } 
}