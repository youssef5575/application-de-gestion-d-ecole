using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Enseignants.Queries;

public record GetEnseignantByIdQuery(Guid Id) : IRequest<EnseignantDto?>;

public class GetEnseignantByIdHandler : IRequestHandler<GetEnseignantByIdQuery, EnseignantDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEnseignantByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EnseignantDto?> Handle(GetEnseignantByIdQuery request, CancellationToken cancellationToken)
    {
        var enseignant = await _unitOfWork.EnseignantRepository.GetByIdAsync(request.Id, cancellationToken);
        return enseignant == null ? null : _mapper.Map<EnseignantDto>(enseignant);
    }
}