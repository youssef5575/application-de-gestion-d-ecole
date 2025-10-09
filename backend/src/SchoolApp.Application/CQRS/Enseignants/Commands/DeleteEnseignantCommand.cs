using MediatR;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Enseignants.Commands;

public record DeleteEnseignantCommand(Guid Id) : IRequest<Unit>;

public class DeleteEnseignantHandler : IRequestHandler<DeleteEnseignantCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEnseignantHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteEnseignantCommand request, CancellationToken cancellationToken)
    {
        var enseignant = await _unitOfWork.EnseignantRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enseignant == null)
        {
            throw new Exception($"Enseignant with ID {request.Id} not found");
        }

        _unitOfWork.EnseignantRepository.Remove(enseignant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}