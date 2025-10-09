using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Enseignants.Queries;

public record GetAllEnseignantsQuery() : IRequest<IEnumerable<EnseignantDto>>;

public class GetAllEnseignantsHandler : IRequestHandler<GetAllEnseignantsQuery, IEnumerable<EnseignantDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllEnseignantsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EnseignantDto>> Handle(GetAllEnseignantsQuery request, CancellationToken cancellationToken)
    {
        var enseignants = await _unitOfWork.EnseignantRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<EnseignantDto>>(enseignants);
    }
}