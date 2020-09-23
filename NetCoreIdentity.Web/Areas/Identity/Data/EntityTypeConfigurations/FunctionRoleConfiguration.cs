using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations
{
    public class FunctionRoleConfiguration : IEntityTypeConfiguration<FunctionRole>
    {

        public void Configure(EntityTypeBuilder<FunctionRole> builder)
        {
            // Rename table
            builder.ToTable("NetCoreFunctionRole");
            builder.HasKey(x => new { x.AppFunctionId, x.RoleId });

            builder
                .HasOne(m => m.Function)
                .WithMany(fr => fr.FunctionRoles)
                .HasForeignKey(k => k.AppFunctionId);

            builder
               .HasOne(m => m.Role)
               .WithMany(fr => fr.FunctionRoles)
               .HasForeignKey(k => k.RoleId);

        }
    }
}
