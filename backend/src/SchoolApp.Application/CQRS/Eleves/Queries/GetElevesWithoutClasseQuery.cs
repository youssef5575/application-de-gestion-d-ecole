using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Eleves.Queries;

public record GetElevesWithoutClasseQuery() : IRequest<IEnumerable<EleveDto>>;

public class GetElevesWithoutClasseHandler : IRequestHandler<GetElevesWithoutClasseQuery, IEnumerable<EleveDto>>
{
    private readonly IAffectationService _affectationService;
    private readonly IMapper _mapper;

    public GetElevesWithoutClasseHandler(IAffectationService affectationService, IMapper mapper)
    {
        _affectationService = affectationService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EleveDto>> Handle(GetElevesWithoutClasseQuery request, CancellationToken cancellationToken)
    {
        var eleves = await _affectationService.GetElevesWithoutClasseAsync(cancellationToken);
        return _mapper.Map<IEnumerable<EleveDto>>(eleves);
    }
}