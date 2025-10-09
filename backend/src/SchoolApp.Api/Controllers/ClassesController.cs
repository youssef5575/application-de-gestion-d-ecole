using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.CQRS.Classes.Commands;
using SchoolApp.Application.CQRS.Classes.Queries;
using SchoolApp.Application.DTOs.Classe;

namespace SchoolApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClassesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllClassesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetClasseByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = $"Classe with ID {id} not found" });
        }
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ClasseCreateDto dto)
    {
        var id = await _mediator.Send(new CreateClasseCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClasseUpdateDto dto)
    {
        await _mediator.Send(new UpdateClasseCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteClasseCommand(id));
        return NoContent();
    }
}