namespace SchoolApp.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Entities.Eleve> EleveRepository { get; }
    IRepository<Entities.Enseignant> EnseignantRepository { get; }
    IRepository<Entities.Matiere> MatiereRepository { get; }
    IRepository<Entities.Classe> ClasseRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}