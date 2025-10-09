using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Entities;
using SchoolApp.Infrastructure.Identity;

namespace SchoolApp.Infrastructure.Data;

public class SchoolDbContext : IdentityDbContext<ApplicationUser>
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    public DbSet<Eleve> Eleves => Set<Eleve>();
    public DbSet<Enseignant> Enseignants => Set<Enseignant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Eleve>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Matricule).IsUnique();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Matricule).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(20);
            entity.Property(e => e.Adresse).HasMaxLength(200);
        });

        modelBuilder.Entity<Enseignant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Matricule).IsUnique();
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Matricule).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(20);
            entity.Property(e => e.Specialite).HasMaxLength(100);
        });
    }
}