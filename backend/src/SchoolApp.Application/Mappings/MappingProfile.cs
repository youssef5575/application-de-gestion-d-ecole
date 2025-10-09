using AutoMapper;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Eleve mappings
        CreateMap<Eleve, EleveDto>();
        CreateMap<EleveCreateDto, Eleve>();
        CreateMap<EleveUpdateDto, Eleve>();

        // Enseignant mappings
        CreateMap<Enseignant, EnseignantDto>();
        CreateMap<EnseignantCreateDto, Enseignant>();
        CreateMap<EnseignantUpdateDto, Enseignant>();
    }
}