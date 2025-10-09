namespace SchoolApp.Application.DTOs.Classe;

public class ClasseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public string? Niveau { get; set; }
    public int? CapaciteMax { get; set; }
}