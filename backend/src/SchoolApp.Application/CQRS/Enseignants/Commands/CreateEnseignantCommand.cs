using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolApp.Application.DTOs.Enseignant;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Interfaces;
using SchoolApp.Infrastructure.Identity;

namespace SchoolApp.Application.CQRS.Enseignants.Commands;

public record CreateEnseignantCommand(EnseignantCreateDto Dto) : IRequest<Guid>;

public class CreateEnseignantHandler : IRequestHandler<CreateEnseignantCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CreateEnseignantHandler(
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

    public async Task<Guid> Handle(CreateEnseignantCommand request, CancellationToken cancellationToken)
    {
        var enseignant = _mapper.Map<Enseignant>(request.Dto);

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
                // Create Enseignant role if it doesn't exist
                if (!await _roleManager.RoleExistsAsync("Enseignant"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Enseignant"));
                }

                await _userManager.AddToRoleAsync(user, "Enseignant");
                enseignant.UserId = user.Id;
            }
            else
            {
                // Log the errors for debugging
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user account: {errors}");
            }
        }

        await _unitOfWork.EnseignantRepository.AddAsync(enseignant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return enseignant.Id;
    }
}