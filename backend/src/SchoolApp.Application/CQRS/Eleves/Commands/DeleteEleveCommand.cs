using MediatR;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Eleves.Commands;

public record DeleteEleveCommand(Guid Id) : IRequest<Unit>;

public class DeleteEleveHandler : IRequestHandler<DeleteEleveCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEleveHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteEleveCommand request, CancellationToken cancellationToken)
    {
        var eleve = await _unitOfWork.EleveRepository.GetByIdAsync(request.Id, cancellationToken);
        if (eleve == null)
        {
            throw new Exception($"Eleve with ID {request.Id} not found");
        }

        _unitOfWork.EleveRepository.Remove(eleve);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}