using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCoreIdentity.Web.Areas.Identity.Data;
using NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations;

namespace NetCoreIdentity.Web
{
    public class NetCoreIdentityContext : IdentityDbContext<NetCoreIdentityUser, NetCoreIdentityRole, string,
        IdentityUserClaim<string>, NetCoreIdentityUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public NetCoreIdentityContext(DbContextOptions<NetCoreIdentityContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        public virtual DbSet<AppFunction> AppFunction { get; set; }
        public virtual DbSet<FunctionRole> FunctionRole { get; set; }
        public IConfiguration Configuration { get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var identitySetting = Configuration.GetSection("IdentitySettings");
            string adminUserId = identitySetting.GetSection("AdminUserId").Value;
            string adminRoleId = identitySetting.GetSection("AdminRoleId").Value;

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.ApplyConfiguration(new IdentityUserConfiguration(adminUserId));
            builder.ApplyConfiguration(new IdentityRoleConfiguration(adminRoleId));
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration(adminUserId, adminRoleId));
            builder.ApplyConfiguration(new AppFunctionConfiguration());
            builder.ApplyConfiguration(new FunctionRoleConfiguration());
        }
    }
}
