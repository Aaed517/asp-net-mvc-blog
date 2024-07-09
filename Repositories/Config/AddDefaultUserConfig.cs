using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.Config
{
    public class AddDefaultUserConfig : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var users = new List<IdentityUser>();

            for (int i = 1; i <= 40; i++)
            {
                var username = $"User{i}";
                var user = new IdentityUser
                {
                    Id = $"TestUserId{i}",
                    UserName = username,
                    NormalizedUserName = username.ToUpper(),
                    Email = $"{username}@example.com",
                };
                user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "Passwd123*");


                users.Add(user);
            }
            builder.HasData(users);
        }
    }
}
