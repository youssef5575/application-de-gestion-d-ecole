using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Enseignants.Commands;

public record UpdateEnseignantCommand(Guid Id, EnseignantUpdateDto Dto) : IRequest<Unit>;

public class UpdateEnseignantHandler : IRequestHandler<UpdateEnseignantCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEnseignantHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEnseignantCommand request, CancellationToken cancellationToken)
    {
        var enseignant = await _unitOfWork.EnseignantRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enseignant == null)
        {
            throw new Exception($"Enseignant with ID {request.Id} not found");
        }

        _mapper.Map(request.Dto, enseignant);
        enseignant.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.EnseignantRepository.Update(enseignant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}