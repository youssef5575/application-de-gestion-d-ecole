using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.CQRS.Enseignants.Commands;
using SchoolApp.Application.CQRS.Enseignants.Queries;
using SchoolApp.Application.DTOs.Enseignant;

namespace SchoolApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnseignantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnseignantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEnseignantsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEnseignantByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = $"Enseignant with ID {id} not found" });
        }
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] EnseignantCreateDto dto)
    {
        var id = await _mediator.Send(new CreateEnseignantCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EnseignantUpdateDto dto)
    {
        await _mediator.Send(new UpdateEnseignantCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEnseignantCommand(id));
        return NoContent();
    }
}