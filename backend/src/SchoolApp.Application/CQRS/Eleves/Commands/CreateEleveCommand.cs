using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Eleves.Commands;

public record CreateEleveCommand(EleveCreateDto Dto) : IRequest<Guid>;

public class CreateEleveHandler : IRequestHandler<CreateEleveCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEleveHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEleveCommand request, CancellationToken cancellationToken)
    {
        var eleve = _mapper.Map<Eleve>(request.Dto);
        await _unitOfWork.EleveRepository.AddAsync(eleve, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return eleve.Id;
    }
}