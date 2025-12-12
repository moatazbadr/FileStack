using FileStack.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStack.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
                new IdentityRole
                {
                    Id = "c3de2627-8498-4c57-8a60-af005eb15853",
                    Name = ValidUserRoles.Admin,
                    NormalizedName = ValidUserRoles.Admin.ToUpper()
                },
                new IdentityRole
                {
                    Id = "93222615-3dcf-4d6a-8c7d-0829eb14defa",
                    Name = ValidUserRoles.User,
                    NormalizedName = ValidUserRoles.User.ToUpper()
                }
            );

        }
    }
}
