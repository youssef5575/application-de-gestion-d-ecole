namespace SchoolApp.Application.DTOs.Affectation;

public class AssignEleveToClasseDto
{
    public Guid EleveId { get; set; }
    public Guid? ClasseId { get; set; }
}