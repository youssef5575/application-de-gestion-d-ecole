using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Eleves.Queries;

public record GetAllElevesQuery() : IRequest<IEnumerable<EleveDto>>;

public class GetAllElevesHandler : IRequestHandler<GetAllElevesQuery, IEnumerable<EleveDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllElevesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EleveDto>> Handle(GetAllElevesQuery request, CancellationToken cancellationToken)
    {
        var eleves = await _unitOfWork.EleveRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<EleveDto>>(eleves);
    }
}