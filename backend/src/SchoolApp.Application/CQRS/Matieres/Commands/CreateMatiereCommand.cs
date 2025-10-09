using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Matiere;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Matieres.Commands;

public record CreateMatiereCommand(MatiereCreateDto Dto) : IRequest<Guid>;

public class CreateMatiereHandler : IRequestHandler<CreateMatiereCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMatiereHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateMatiereCommand request, CancellationToken cancellationToken)
    {
        var matiere = _mapper.Map<Matiere>(request.Dto);
        await _unitOfWork.MatiereRepository.AddAsync(matiere, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return matiere.Id;
    }
}