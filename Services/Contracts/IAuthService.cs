using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Entities.Dtos;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get;}
        IEnumerable<IdentityUser> UsersWithRoles();
        UserWithRolesViewModel GetOneUserWithRole(string userName);
        Task UpdateOneUser(UserWithRolesViewModel user);
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto);
        Task<IdentityUser> GetOneUser(string userId);
        Task Update(UserDtoForUpdate userDto);
        Task<IdentityResult> DeleteOneUser(string userName);

    }
}