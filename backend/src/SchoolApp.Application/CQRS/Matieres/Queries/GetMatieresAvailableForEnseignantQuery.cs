using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Matiere;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Matieres.Queries;

public record GetMatieresAvailableForEnseignantQuery(Guid EnseignantId) : IRequest<IEnumerable<MatiereDto>>;

public class GetMatieresAvailableForEnseignantHandler : IRequestHandler<GetMatieresAvailableForEnseignantQuery, IEnumerable<MatiereDto>>
{
    private readonly IAffectationService _affectationService;
    private readonly IMapper _mapper;

    public GetMatieresAvailableForEnseignantHandler(IAffectationService affectationService, IMapper mapper)
    {
        _affectationService = affectationService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MatiereDto>> Handle(GetMatieresAvailableForEnseignantQuery request, CancellationToken cancellationToken)
    {
        var matieres = await _affectationService.GetMatieresAvailableForEnseignantAsync(request.EnseignantId, cancellationToken);
        return _mapper.Map<IEnumerable<MatiereDto>>(matieres);
    }
}