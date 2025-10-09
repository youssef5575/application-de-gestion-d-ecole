using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Classe;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Classes.Commands;

public record CreateClasseCommand(ClasseCreateDto Dto) : IRequest<Guid>;

public class CreateClasseHandler : IRequestHandler<CreateClasseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateClasseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateClasseCommand request, CancellationToken cancellationToken)
    {
        var classe = _mapper.Map<Classe>(request.Dto);
        await _unitOfWork.ClasseRepository.AddAsync(classe, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return classe.Id;
    }
}