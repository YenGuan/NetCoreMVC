using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations
{
    public class AppFunctionConfiguration : IEntityTypeConfiguration<AppFunction>
    {

        public void Configure(EntityTypeBuilder<AppFunction> builder)
        {
            // Rename table
            builder.ToTable("NetCoreAppFunction");
            builder.Property(e => e.AppFunctionId).HasColumnName("AppFunctionID");
            builder.Property(e => e.AppFunctionName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.ControllerName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.SchemaName)
                .HasMaxLength(50);
        } 
    }

}
