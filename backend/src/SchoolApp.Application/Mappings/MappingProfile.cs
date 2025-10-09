using AutoMapper;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Application.DTOs.Matiere;
using SchoolApp.Application.DTOs.Classe;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Eleve, EleveDto>();
        CreateMap<EleveCreateDto, Eleve>();
        CreateMap<EleveUpdateDto, Eleve>();

        CreateMap<Enseignant, EnseignantDto>();
        CreateMap<EnseignantCreateDto, Enseignant>();
        CreateMap<EnseignantUpdateDto, Enseignant>();

        CreateMap<Matiere, MatiereDto>();
        CreateMap<MatiereCreateDto, Matiere>();
        CreateMap<MatiereUpdateDto, Matiere>();

        CreateMap<Classe, ClasseDto>();
        CreateMap<ClasseCreateDto, Classe>();
        CreateMap<ClasseUpdateDto, Classe>();
    }
}