using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Matiere;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Matieres.Commands;

public record UpdateMatiereCommand(Guid Id, MatiereUpdateDto Dto) : IRequest<Unit>;

public class UpdateMatiereHandler : IRequestHandler<UpdateMatiereCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMatiereHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMatiereCommand request, CancellationToken cancellationToken)
    {
        var matiere = await _unitOfWork.MatiereRepository.GetByIdAsync(request.Id, cancellationToken);
        if (matiere == null)
        {
            throw new Exception($"Matiere with ID {request.Id} not found");
        }

        _mapper.Map(request.Dto, matiere);
        matiere.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.MatiereRepository.Update(matiere);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}