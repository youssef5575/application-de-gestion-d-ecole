namespace SchoolApp.Application.DTOs.Classe;

public class ClasseWithElevesDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public string? Niveau { get; set; }
    public int? CapaciteMax { get; set; }
    public List<EleveSimpleDto> Eleves { get; set; } = new();
}

public class EleveSimpleDto
{
    public Guid Id { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
}