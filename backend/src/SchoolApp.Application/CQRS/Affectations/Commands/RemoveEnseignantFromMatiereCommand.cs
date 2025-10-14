using MediatR;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Affectations.Commands;

public record RemoveEnseignantFromMatiereCommand(Guid EnseignantId, Guid MatiereId) : IRequest<Unit>;

public class RemoveEnseignantFromMatiereHandler : IRequestHandler<RemoveEnseignantFromMatiereCommand, Unit>
{
    private readonly IAffectationService _affectationService;

    public RemoveEnseignantFromMatiereHandler(IAffectationService affectationService)
    {
        _affectationService = affectationService;
    }

    public async Task<Unit> Handle(RemoveEnseignantFromMatiereCommand request, CancellationToken cancellationToken)
    {
        await _affectationService.RemoveEnseignantFromMatiereAsync(
            request.EnseignantId,
            request.MatiereId,
            cancellationToken);

        return Unit.Value;
    }
}