using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Eleves.Commands;

public record UpdateEleveCommand(Guid Id, EleveUpdateDto Dto) : IRequest<Unit>;

public class UpdateEleveHandler : IRequestHandler<UpdateEleveCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEleveHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEleveCommand request, CancellationToken cancellationToken)
    {
        var eleve = await _unitOfWork.EleveRepository.GetByIdAsync(request.Id, cancellationToken);
        if (eleve == null)
        {
            throw new Exception($"Eleve with ID {request.Id} not found");
        }

        _mapper.Map(request.Dto, eleve);
        eleve.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.EleveRepository.Update(eleve);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}