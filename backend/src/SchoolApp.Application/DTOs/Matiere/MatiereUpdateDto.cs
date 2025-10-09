namespace SchoolApp.Application.DTOs.Matiere;

public class MatiereUpdateDto
{
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? Coefficient { get; set; }
}