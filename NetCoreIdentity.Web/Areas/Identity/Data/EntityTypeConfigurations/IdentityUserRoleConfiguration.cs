using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<NetCoreIdentityUserRole>
    {

        public void Configure(EntityTypeBuilder<NetCoreIdentityUserRole> builder)
        {
            // Rename table
            builder.ToTable("NetCoreUserRoles");
        }
    }
}
