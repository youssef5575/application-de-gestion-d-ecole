namespace SchoolApp.Application.DTOs.Enseignant;

public class EnseignantWithMatieresDto
{
    public Guid Id { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Specialite { get; set; }
    public List<MatiereSimpleDto> Matieres { get; set; } = new();
}

public class MatiereSimpleDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
}