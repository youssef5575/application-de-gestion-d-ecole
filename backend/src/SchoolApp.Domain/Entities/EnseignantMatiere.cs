namespace SchoolApp.Domain.Entities;

public class EnseignantMatiere
{
    public Guid EnseignantId { get; set; }
    public Enseignant Enseignant { get; set; } = null!;
    
    public Guid MatiereId { get; set; }
    public Matiere Matiere { get; set; } = null!;
}