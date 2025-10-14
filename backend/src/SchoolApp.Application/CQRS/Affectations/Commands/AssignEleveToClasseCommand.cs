using MediatR;
using SchoolApp.Application.DTOs.Affectation;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Affectations.Commands;

public record AssignEleveToClasseCommand(AssignEleveToClasseDto Dto) : IRequest<Unit>;

public class AssignEleveToClasseHandler : IRequestHandler<AssignEleveToClasseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignEleveToClasseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AssignEleveToClasseCommand request, CancellationToken cancellationToken)
    {
        var eleve = await _unitOfWork.EleveRepository.GetByIdAsync(request.Dto.EleveId, cancellationToken);
        if (eleve == null)
        {
            throw new Exception($"Eleve with ID {request.Dto.EleveId} not found");
        }

        if (request.Dto.ClasseId.HasValue)
        {
            var classe = await _unitOfWork.ClasseRepository.GetByIdAsync(request.Dto.ClasseId.Value, cancellationToken);
            if (classe == null)
            {
                throw new Exception($"Classe with ID {request.Dto.ClasseId} not found");
            }
        }

        eleve.ClasseId = request.Dto.ClasseId;
        eleve.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.EleveRepository.Update(eleve);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}