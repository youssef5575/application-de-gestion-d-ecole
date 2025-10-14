using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.CQRS.Affectations.Commands;
using SchoolApp.Application.CQRS.Classes.Queries;
using SchoolApp.Application.CQRS.Eleves.Queries;
using SchoolApp.Application.CQRS.Enseignants.Queries;
using SchoolApp.Application.CQRS.Matieres.Queries;
using SchoolApp.Application.DTOs.Affectation;

namespace SchoolApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AffectationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AffectationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("eleve-to-classe")]
    public async Task<IActionResult> AssignEleveToClasse([FromBody] AssignEleveToClasseDto dto)
    {
        await _mediator.Send(new AssignEleveToClasseCommand(dto));
        return Ok(new { message = "Élève affecté à la classe avec succès" });
    }

    [HttpPost("enseignant-to-matiere")]
    public async Task<IActionResult> AssignEnseignantToMatiere([FromBody] AssignEnseignantToMatiereDto dto)
    {
        await _mediator.Send(new AssignEnseignantToMatiereCommand(dto));
        return Ok(new { message = "Enseignant affecté à la matière avec succès" });
    }

    [HttpDelete("enseignant-from-matiere/{enseignantId}/{matiereId}")]
    public async Task<IActionResult> RemoveEnseignantFromMatiere(Guid enseignantId, Guid matiereId)
    {
        await _mediator.Send(new RemoveEnseignantFromMatiereCommand(enseignantId, matiereId));
        return Ok(new { message = "Affectation supprimée avec succès" });
    }

    [HttpGet("classe/{classeId}/eleves")]
    public async Task<IActionResult> GetClasseWithEleves(Guid classeId)
    {
        var result = await _mediator.Send(new GetClasseWithElevesQuery(classeId));
        if (result == null)
        {
            return NotFound(new { message = "Classe non trouvée" });
        }
        return Ok(result);
    }

    [HttpGet("enseignant/{enseignantId}/matieres")]
    public async Task<IActionResult> GetEnseignantWithMatieres(Guid enseignantId)
    {
        var result = await _mediator.Send(new GetEnseignantWithMatieresQuery(enseignantId));
        if (result == null)
        {
            return NotFound(new { message = "Enseignant non trouvé" });
        }
        return Ok(result);
    }

    [HttpGet("eleves/without-classe")]
    public async Task<IActionResult> GetElevesWithoutClasse()
    {
        var result = await _mediator.Send(new GetElevesWithoutClasseQuery());
        return Ok(result);
    }

    [HttpGet("matieres/available/{enseignantId}")]
    public async Task<IActionResult> GetMatieresAvailableForEnseignant(Guid enseignantId)
    {
        var result = await _mediator.Send(new GetMatieresAvailableForEnseignantQuery(enseignantId));
        return Ok(result);
    }
}