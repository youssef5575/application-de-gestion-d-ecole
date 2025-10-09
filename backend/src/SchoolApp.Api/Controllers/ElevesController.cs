using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.CQRS.Eleves.Commands;
using SchoolApp.Application.CQRS.Eleves.Queries;
using SchoolApp.Application.DTOs.Eleve;

namespace SchoolApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ElevesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ElevesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllElevesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEleveByIdQuery(id));
        if (result == null)
        {
            return NotFound(new { message = $"Eleve with ID {id} not found" });
        }
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Create([FromBody] EleveCreateDto dto)
    {
        var id = await _mediator.Send(new CreateEleveCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EleveUpdateDto dto)
    {
        await _mediator.Send(new UpdateEleveCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteEleveCommand(id));
        return NoContent();
    }
}