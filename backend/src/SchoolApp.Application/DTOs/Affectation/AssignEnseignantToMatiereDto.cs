namespace SchoolApp.Application.DTOs.Affectation;

public class AssignEnseignantToMatiereDto
{
    public Guid EnseignantId { get; set; }
    public Guid MatiereId { get; set; }
}