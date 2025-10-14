namespace SchoolApp.Domain.Interfaces;

public interface IAffectationService
{
    Task AssignEnseignantToMatiereAsync(Guid enseignantId, Guid matiereId, CancellationToken ct = default);
    Task RemoveEnseignantFromMatiereAsync(Guid enseignantId, Guid matiereId, CancellationToken ct = default);
    Task<IEnumerable<dynamic>> GetEnseignantWithMatieresAsync(Guid enseignantId, CancellationToken ct = default);
    Task<IEnumerable<dynamic>> GetClasseWithElevesAsync(Guid classeId, CancellationToken ct = default);
    Task<IEnumerable<Entities.Eleve>> GetElevesWithoutClasseAsync(CancellationToken ct = default);
    Task<IEnumerable<Entities.Matiere>> GetMatieresAvailableForEnseignantAsync(Guid enseignantId, CancellationToken ct = default);
}