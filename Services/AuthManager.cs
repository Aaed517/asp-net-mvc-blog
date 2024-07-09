using AutoMapper;
using Bogus.DataSets;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => _roleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            var user = _mapper.Map<IdentityUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if(!result.Succeeded)
            {
                return result;
            }
            if(userDto.Roles.Count > 0)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, userDto.Roles.First());
                if(!roleResult.Succeeded)
                    throw new Exception("Role Error");
                
            }
            return result;
        }

        public UserWithRolesViewModel GetOneUserWithRole(string userName)
        {
            var user = _userManager.Users
                .Where(u => u.UserName == userName)
                .Select(u => new UserWithRolesViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result.ToList()
                })
                    .FirstOrDefault();

            return user;
        }

        public async Task<IdentityUser> GetOneUser(string? userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
                return user;
            throw new Exception("User could not be found.");
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
           var user = await GetOneUser(userDto.UserId);
           user.Email = userDto.Email;
           user.UserName = userDto.UserName;
            var result = await _userManager.UpdateAsync(user);
            if (userDto.Roles.Count > 0)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles);
                var r2 = await _userManager.AddToRolesAsync(user, userDto.Roles);
            }
            return;
        }

        public async Task UpdateOneUser(UserWithRolesViewModel user)
        {
            var User = await _userManager.FindByIdAsync(user.UserId);

            if (User != null)
            {
                User.Email = user.Email;
                User.UserName = user.UserName;
                var result = await _userManager.UpdateAsync(User);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to update user.");
                }     
            }  
            else
            {
                throw new Exception("User not found.");
            }
        }
        public IEnumerable<IdentityUser> UsersWithRoles()
        {
            var usersWithRoles = _userManager.Users
           .Select(u => new UserWithRolesViewModel
           {
               UserId = u.Id,
               UserName = u.UserName,
               Email = u.Email,
               Roles = _userManager.GetRolesAsync(u).Result.ToList()
           })
           .ToList();

            return usersWithRoles;
        }

        public async Task<IdentityResult> DeleteOneUser(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            return await _userManager.DeleteAsync(user);
        }

    }
}