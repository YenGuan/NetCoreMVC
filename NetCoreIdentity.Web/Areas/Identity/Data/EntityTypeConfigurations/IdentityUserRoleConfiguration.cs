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
        private readonly string _adminUserId;
        private readonly string _adminRoleId;
        public IdentityUserRoleConfiguration(string adminUserId, string adminRoleId)
        {
            _adminUserId = adminUserId;
            _adminRoleId = adminRoleId;
        }
        public void Configure(EntityTypeBuilder<NetCoreIdentityUserRole> builder)
        {
            // Rename table
            builder.ToTable("NetCoreUserRoles");

            builder.HasData(
                    new NetCoreIdentityUserRole() { RoleId = _adminRoleId, UserId = _adminUserId }
                );
        }
    }
}
