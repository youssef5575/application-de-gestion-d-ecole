using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.CQRS.Matieres.Commands;
using SchoolApp.Application.CQRS.Matieres.Queries;
using SchoolApp.Application.DTOs.Matiere;

namespace SchoolApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MatieresController : ControllerBase
{
    private readonly IMediator _mediator;

    public MatieresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMatieresQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMatiereByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = $"Matiere with ID {id} not found" });
        }
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] MatiereCreateDto dto)
    {
        var id = await _mediator.Send(new CreateMatiereCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MatiereUpdateDto dto)
    {
        await _mediator.Send(new UpdateMatiereCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteMatiereCommand(id));
        return NoContent();
    }
}