using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;
using SchoolApp.Infrastructure.Data;

namespace SchoolApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _context;
    private IRepository<Eleve>? _eleveRepository;
    private IRepository<Enseignant>? _enseignantRepository;

    public UnitOfWork(SchoolDbContext context)
    {
        _context = context;
    }

    public IRepository<Eleve> EleveRepository =>
        _eleveRepository ??= new EfRepository<Eleve>(_context);

    public IRepository<Enseignant> EnseignantRepository =>
        _enseignantRepository ??= new EfRepository<Enseignant>(_context);

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}