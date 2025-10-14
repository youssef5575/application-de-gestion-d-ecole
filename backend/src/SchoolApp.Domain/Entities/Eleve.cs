namespace SchoolApp.Domain.Entities;


public class Eleve
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string? UserId { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public DateTime? DateNaissance { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? Adresse { get; set; }
    public Guid? ClasseId { get; set; }
    public Classe? Classe { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}