using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<NetCoreIdentityRole>
    {
        public void Configure(EntityTypeBuilder<NetCoreIdentityRole> builder)
        {
            // Rename table
            builder.ToTable("NetCoreRoles");

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

        }
    }
}
