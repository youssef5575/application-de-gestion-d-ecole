using MediatR;
using SchoolApp.Application.DTOs.Classe;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Classes.Queries;

public record GetClasseWithElevesQuery(Guid Id) : IRequest<ClasseWithElevesDto?>;

public class GetClasseWithElevesHandler : IRequestHandler<GetClasseWithElevesQuery, ClasseWithElevesDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAffectationService _affectationService;

    public GetClasseWithElevesHandler(IUnitOfWork unitOfWork, IAffectationService affectationService)
    {
        _unitOfWork = unitOfWork;
        _affectationService = affectationService;
    }

    public async Task<ClasseWithElevesDto?> Handle(GetClasseWithElevesQuery request, CancellationToken cancellationToken)
    {
        var classe = await _unitOfWork.ClasseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (classe == null)
            return null;

        var eleves = await _affectationService.GetClasseWithElevesAsync(request.Id, cancellationToken);

        return new ClasseWithElevesDto
        {
            Id = classe.Id,
            Code = classe.Code,
            Libelle = classe.Libelle,
            Niveau = classe.Niveau,
            CapaciteMax = classe.CapaciteMax,
            Eleves = eleves.Select(e => new EleveSimpleDto
            {
                Id = (Guid)((dynamic)e).Id,
                Matricule = ((dynamic)e).Matricule,
                Nom = ((dynamic)e).Nom,
                Prenom = ((dynamic)e).Prenom
            }).ToList()
        };
    }
}