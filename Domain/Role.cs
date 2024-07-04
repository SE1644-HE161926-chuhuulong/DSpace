using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Role : IdentityRole<string>
{
    public ICollection<UserRole> UserRoles { get; set; }
}