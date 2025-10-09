using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Eleves.Queries;

public record GetEleveByIdQuery(Guid Id) : IRequest<EleveDto?>;

public class GetEleveByIdHandler : IRequestHandler<GetEleveByIdQuery, EleveDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEleveByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EleveDto?> Handle(GetEleveByIdQuery request, CancellationToken cancellationToken)
    {
        var eleve = await _unitOfWork.EleveRepository.GetByIdAsync(request.Id, cancellationToken);
        return eleve == null ? null : _mapper.Map<EleveDto>(eleve);
    }
}