using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Matiere;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Matieres.Queries;

public record GetAllMatieresQuery() : IRequest<IEnumerable<MatiereDto>>;

public class GetAllMatieresHandler : IRequestHandler<GetAllMatieresQuery, IEnumerable<MatiereDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMatieresHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MatiereDto>> Handle(GetAllMatieresQuery request, CancellationToken cancellationToken)
    {
        var matieres = await _unitOfWork.MatiereRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<MatiereDto>>(matieres);
    }
}