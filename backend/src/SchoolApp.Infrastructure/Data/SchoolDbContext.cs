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
    public DbSet<Matiere> Matieres => Set<Matiere>();
    public DbSet<Classe> Classes => Set<Classe>();
    public DbSet<EnseignantMatiere> EnseignantMatieres => Set<EnseignantMatiere>();

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
            
            entity.HasOne<Classe>()
                .WithMany()
                .HasForeignKey(e => e.ClasseId)
                .OnDelete(DeleteBehavior.SetNull);
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

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => m.Code).IsUnique();
            entity.Property(m => m.Code).IsRequired().HasMaxLength(50);
            entity.Property(m => m.Libelle).IsRequired().HasMaxLength(200);
            entity.Property(m => m.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<Classe>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.Code).IsUnique();
            entity.Property(c => c.Code).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Libelle).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Niveau).HasMaxLength(50);
        });

        modelBuilder.Entity<EnseignantMatiere>(entity =>
        {
            entity.HasKey(em => new { em.EnseignantId, em.MatiereId });
            
            entity.HasOne(em => em.Enseignant)
                .WithMany()
                .HasForeignKey(em => em.EnseignantId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(em => em.Matiere)
                .WithMany()
                .HasForeignKey(em => em.MatiereId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}