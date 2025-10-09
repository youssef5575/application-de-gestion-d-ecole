using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Classe;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Classes.Commands;

public record UpdateClasseCommand(Guid Id, ClasseUpdateDto Dto) : IRequest<Unit>;

public class UpdateClasseHandler : IRequestHandler<UpdateClasseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateClasseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateClasseCommand request, CancellationToken cancellationToken)
    {
        var classe = await _unitOfWork.ClasseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (classe == null)
        {
            throw new Exception($"Classe with ID {request.Id} not found");
        }

        _mapper.Map(request.Dto, classe);
        classe.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.ClasseRepository.Update(classe);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}