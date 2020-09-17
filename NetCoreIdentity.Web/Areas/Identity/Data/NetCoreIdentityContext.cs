using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentity.Web.Areas.Identity.Data;
using NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations;

namespace NetCoreIdentity.Web
{
    public class NetCoreIdentityContext : IdentityDbContext<NetCoreIdentityUser, NetCoreIdentityRole, string,
        IdentityUserClaim<string>, NetCoreIdentityUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public NetCoreIdentityContext(DbContextOptions<NetCoreIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.ApplyConfiguration(new IdentityUserConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
        }
    }
}
