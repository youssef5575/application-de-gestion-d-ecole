using AutoMapper;
using MediatR;
using SchoolApp.Application.DTOs.Classe;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Classes.Queries;

public record GetAllClassesQuery() : IRequest<IEnumerable<ClasseDto>>;

public class GetAllClassesHandler : IRequestHandler<GetAllClassesQuery, IEnumerable<ClasseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllClassesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClasseDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
    {
        var classes = await _unitOfWork.ClasseRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ClasseDto>>(classes);
    }
}