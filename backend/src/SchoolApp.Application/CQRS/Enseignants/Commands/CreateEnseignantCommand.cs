using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Enseignants.Commands;

public record CreateEnseignantCommand(EnseignantCreateDto Dto) : IRequest<Guid>;

public class CreateEnseignantHandler : IRequestHandler<CreateEnseignantCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEnseignantHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEnseignantCommand request, CancellationToken cancellationToken)
    {
        var enseignant = _mapper.Map<Enseignant>(request.Dto);
        await _unitOfWork.EnseignantRepository.AddAsync(enseignant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return enseignant.Id;
    }
}