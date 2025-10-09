namespace SchoolApp.Application.DTOs.Eleve;

public class EleveDto
{
    public Guid Id { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public DateTime? DateNaissance { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? Adresse { get; set; }
    public Guid? ClasseId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}