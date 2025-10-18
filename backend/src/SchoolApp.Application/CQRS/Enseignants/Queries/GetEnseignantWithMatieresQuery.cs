using MediatR;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Interfaces;

namespace SchoolApp.Application.CQRS.Enseignants.Queries;

public record GetEnseignantWithMatieresQuery(Guid Id) : IRequest<EnseignantWithMatieresDto?>;

public class GetEnseignantWithMatieresHandler : IRequestHandler<GetEnseignantWithMatieresQuery, EnseignantWithMatieresDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAffectationService _affectationService;

    public GetEnseignantWithMatieresHandler(IUnitOfWork unitOfWork, IAffectationService affectationService)
    {
        _unitOfWork = unitOfWork;
        _affectationService = affectationService;
    }

    public async Task<EnseignantWithMatieresDto?> Handle(GetEnseignantWithMatieresQuery request, CancellationToken cancellationToken)
    {
        var enseignant = await _unitOfWork.EnseignantRepository.GetByIdAsync(request.Id, cancellationToken);
        if (enseignant == null)
            return null;

        var matieres = await _affectationService.GetEnseignantWithMatieresAsync(request.Id, cancellationToken);

        return new EnseignantWithMatieresDto
        {
            Id = enseignant.Id,
            Matricule = enseignant.Matricule,
            Nom = enseignant.Nom,
            Prenom = enseignant.Prenom,
            Email = enseignant.Email,
            Telephone = enseignant.Telephone,
            Specialite = enseignant.Specialite,
            Matieres = matieres.Select(m => new MatiereSimpleDto
            {
                Id = m.Id,
                Code = m.Code,
                Libelle = m.Libelle
            }).ToList()
        };
    }
}