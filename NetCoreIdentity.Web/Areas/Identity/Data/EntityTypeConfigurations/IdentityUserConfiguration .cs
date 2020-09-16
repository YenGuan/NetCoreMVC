using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity.Data.EntityTypeConfigurations
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<NetCoreIdentityUser>
    {
        private readonly string _adminUserId;
        public IdentityUserConfiguration(string adminUserId)
        {
            _adminUserId = adminUserId;
        }
        public void Configure(EntityTypeBuilder<NetCoreIdentityUser> builder)
        {
            // Rename table
            builder.ToTable("NetCoreUsers");
            // Each User can have many UserClaims
            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            var hasher = new PasswordHasher<NetCoreIdentityUser>();

            builder.HasData( new NetCoreIdentityUser() 
            {
                Id = _adminUserId, 
                UserName = "crusade771022@hotmail.com",
                NormalizedUserName = "Administrator",
                Email = "crusade771022@hotmail.com",
                NormalizedEmail = "CRUSADE771022@HOTMAIL.COM",
                EmailConfirmed = true               
             
            });

            


        }
    }
}
