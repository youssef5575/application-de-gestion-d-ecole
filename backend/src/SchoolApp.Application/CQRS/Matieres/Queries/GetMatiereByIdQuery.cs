using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Matiere;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Matieres.Queries;

public record GetMatiereByIdQuery(Guid Id) : IRequest<MatiereDto?>;

public class GetMatiereByIdHandler : IRequestHandler<GetMatiereByIdQuery, MatiereDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMatiereByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MatiereDto?> Handle(GetMatiereByIdQuery request, CancellationToken cancellationToken)
    {
        var matiere = await _unitOfWork.MatiereRepository.GetByIdAsync(request.Id, cancellationToken);
        return matiere == null ? null : _mapper.Map<MatiereDto>(matiere);
    }
}