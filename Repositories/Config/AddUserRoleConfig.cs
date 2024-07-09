using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.Config
{
    public class AddUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var userRoles = new List<IdentityUserRole<string>>();

            for (int i = 1; i <= 40; i++)
            {
                var userId = $"TestUserId{i}";
                var roleId = "userRoleID";

                var userRole = new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = roleId
                };

                userRoles.Add(userRole);
            }

            builder.HasData(userRoles);
        }
    }
}