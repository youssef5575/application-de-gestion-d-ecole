using MediatR;
using SchoolApp.Application.DTOs.Affectation;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Affectations.Commands;

public record AssignEnseignantToMatiereCommand(AssignEnseignantToMatiereDto Dto) : IRequest<Unit>;

public class AssignEnseignantToMatiereHandler : IRequestHandler<AssignEnseignantToMatiereCommand, Unit>
{
    private readonly IAffectationService _affectationService;

    public AssignEnseignantToMatiereHandler(IAffectationService affectationService)
    {
        _affectationService = affectationService;
    }

    public async Task<Unit> Handle(AssignEnseignantToMatiereCommand request, CancellationToken cancellationToken)
    {
        await _affectationService.AssignEnseignantToMatiereAsync(
            request.Dto.EnseignantId,
            request.Dto.MatiereId,
            cancellationToken);

        return Unit.Value;
    }
}