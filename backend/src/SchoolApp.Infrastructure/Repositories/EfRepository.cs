using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Interfaces;
using SchoolApp.Infrastructure.Data;

namespace SchoolApp.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly SchoolDbContext _context;

    public EfRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Set<T>().FindAsync(new object[] { id }, ct);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Set<T>().ToListAsync(ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _context.Set<T>().AddAsync(entity, ct);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}