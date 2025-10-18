using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;
using SchoolApp.Infrastructure.Data;

namespace SchoolApp.Infrastructure.Services;

public class AffectationService : IAffectationService
{
    private readonly SchoolDbContext _context;

    public AffectationService(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task AssignEnseignantToMatiereAsync(Guid enseignantId, Guid matiereId, CancellationToken ct = default)
    {
        var exists = await _context.EnseignantMatieres
            .AnyAsync(em => em.EnseignantId == enseignantId && em.MatiereId == matiereId, ct);

        if (exists)
        {
            throw new Exception("Cette affectation existe déjà");
        }

        var enseignantMatiere = new EnseignantMatiere
        {
            EnseignantId = enseignantId,
            MatiereId = matiereId
        };

        _context.EnseignantMatieres.Add(enseignantMatiere);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveEnseignantFromMatiereAsync(Guid enseignantId, Guid matiereId, CancellationToken ct = default)
    {
        var enseignantMatiere = await _context.EnseignantMatieres
            .FirstOrDefaultAsync(em => em.EnseignantId == enseignantId && em.MatiereId == matiereId, ct);

        if (enseignantMatiere == null)
        {
            throw new Exception("Affectation non trouvée");
        }

        _context.EnseignantMatieres.Remove(enseignantMatiere);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<MatiereSimpleInfo>> GetEnseignantWithMatieresAsync(Guid enseignantId, CancellationToken ct = default)
    {
        var enseignant = await _context.Enseignants
            .Include(e => e.EnseignantMatieres)
            .ThenInclude(em => em.Matiere)
            .FirstOrDefaultAsync(e => e.Id == enseignantId, ct);

        if (enseignant == null)
            return Enumerable.Empty<MatiereSimpleInfo>();

        return enseignant.EnseignantMatieres.Select(em => new MatiereSimpleInfo
        {
            Id = em.Matiere.Id,
            Code = em.Matiere.Code,
            Libelle = em.Matiere.Libelle
        });
    }

    public async Task<IEnumerable<dynamic>> GetClasseWithElevesAsync(Guid classeId, CancellationToken ct = default)
    {
        var classe = await _context.Classes
            .Include(c => c.Eleves)
            .FirstOrDefaultAsync(c => c.Id == classeId, ct);

        if (classe == null)
            return Enumerable.Empty<dynamic>();

        return classe.Eleves.Select(e => new
        {
            e.Id,
            e.Matricule,
            e.Nom,
            e.Prenom
        });
    }

    public async Task<IEnumerable<Eleve>> GetElevesWithoutClasseAsync(CancellationToken ct = default)
    {
        return await _context.Eleves
            .Where(e => e.ClasseId == null)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Matiere>> GetMatieresAvailableForEnseignantAsync(Guid enseignantId, CancellationToken ct = default)
    {
        var assignedMatiereIds = await _context.EnseignantMatieres
            .Where(em => em.EnseignantId == enseignantId)
            .Select(em => em.MatiereId)
            .ToListAsync(ct);

        return await _context.Matieres
            .Where(m => !assignedMatiereIds.Contains(m.Id))
            .ToListAsync(ct);
    }
}