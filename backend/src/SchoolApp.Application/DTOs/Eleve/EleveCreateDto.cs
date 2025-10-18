namespace SchoolApp.Application.DTOs.Eleve;

public class EleveCreateDto
{
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public DateTime? DateNaissance { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? Adresse { get; set; }
    public Guid? ClasseId { get; set; }
    public string? Password { get; set; }
}