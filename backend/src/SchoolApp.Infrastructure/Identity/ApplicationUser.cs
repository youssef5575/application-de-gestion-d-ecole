using Microsoft.AspNetCore.Identity;

namespace SchoolApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}