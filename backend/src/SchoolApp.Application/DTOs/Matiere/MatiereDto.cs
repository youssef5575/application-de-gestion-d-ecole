namespace SchoolApp.Application.DTOs.Matiere;

public class MatiereDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? Coefficient { get; set; }
}