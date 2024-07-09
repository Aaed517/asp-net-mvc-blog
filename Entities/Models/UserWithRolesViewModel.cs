using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class UserWithRolesViewModel : IdentityUser
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
    }
}