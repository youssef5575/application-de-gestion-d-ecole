namespace SchoolApp.Application.DTOs.Classe;

public class ClasseCreateDto
{
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public string? Niveau { get; set; }
    public int? CapaciteMax { get; set; }
}