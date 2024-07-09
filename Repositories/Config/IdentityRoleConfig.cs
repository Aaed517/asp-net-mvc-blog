using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            
            builder.HasData(
                new IdentityRole(){Id="userRoleID",Name="User", NormalizedName="USER"},
                new IdentityRole(){Id="adminRoleID",Name="Admin", NormalizedName="ADMIN"}
            );
        }
    }
}