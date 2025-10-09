using MediatR;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Matieres.Commands;

public record DeleteMatiereCommand(Guid Id) : IRequest<Unit>;

public class DeleteMatiereHandler : IRequestHandler<DeleteMatiereCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMatiereHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteMatiereCommand request, CancellationToken cancellationToken)
    {
        var matiere = await _unitOfWork.MatiereRepository.GetByIdAsync(request.Id, cancellationToken);
        if (matiere == null)
        {
            throw new Exception($"Matiere with ID {request.Id} not found");
        }

        _unitOfWork.MatiereRepository.Remove(matiere);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}