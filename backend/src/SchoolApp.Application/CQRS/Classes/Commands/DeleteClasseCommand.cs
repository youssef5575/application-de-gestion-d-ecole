using MediatR;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Classes.Commands;

public record DeleteClasseCommand(Guid Id) : IRequest<Unit>;

public class DeleteClasseHandler : IRequestHandler<DeleteClasseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClasseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteClasseCommand request, CancellationToken cancellationToken)
    {
        var classe = await _unitOfWork.ClasseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (classe == null)
        {
            throw new Exception($"Classe with ID {request.Id} not found");
        }

        _unitOfWork.ClasseRepository.Remove(classe);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}