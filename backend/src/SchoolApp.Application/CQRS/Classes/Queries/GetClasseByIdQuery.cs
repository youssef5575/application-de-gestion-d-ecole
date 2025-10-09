using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Classe;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Classes.Queries;

public record GetClasseByIdQuery(Guid Id) : IRequest<ClasseDto?>;

public class GetClasseByIdHandler : IRequestHandler<GetClasseByIdQuery, ClasseDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClasseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClasseDto?> Handle(GetClasseByIdQuery request, CancellationToken cancellationToken)
    {
        var classe = await _unitOfWork.ClasseRepository.GetByIdAsync(request.Id, cancellationToken);
        return classe == null ? null : _mapper.Map<ClasseDto>(classe);
    }
}