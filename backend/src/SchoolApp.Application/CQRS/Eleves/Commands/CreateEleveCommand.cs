using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolApp.Application.DTOs.Eleve;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;
using SchoolApp.Infrastructure.Identity;

namespace SchoolApp.Application.CQRS.Eleves.Commands;

public record CreateEleveCommand(EleveCreateDto Dto) : IRequest<Guid>;

public class CreateEleveHandler : IRequestHandler<CreateEleveCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateEleveHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Guid> Handle(CreateEleveCommand request, CancellationToken cancellationToken)
    {
        var eleve = _mapper.Map<Eleve>(request.Dto);

        // Create user account if email and password are provided
        if (!string.IsNullOrWhiteSpace(request.Dto.Email) && !string.IsNullOrWhiteSpace(request.Dto.Password))
        {
            var user = new ApplicationUser
            {
                UserName = request.Dto.Email,
                Email = request.Dto.Email,
                EmailConfirmed = true,
                FullName = $"{request.Dto.Prenom} {request.Dto.Nom}"
            };

            var result = await _userManager.CreateAsync(user, request.Dto.Password);
            if (result.Succeeded)
            {
                // Create Eleve role if it doesn't exist
                if (!await _roleManager.RoleExistsAsync("Eleve"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Eleve"));
                }

                await _userManager.AddToRoleAsync(user, "Eleve");
                eleve.UserId = user.Id;
            }
            else
            {
                // Log the errors for debugging
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user account: {errors}");
            }
        }

        await _unitOfWork.EleveRepository.AddAsync(eleve, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return eleve.Id;
    }
}