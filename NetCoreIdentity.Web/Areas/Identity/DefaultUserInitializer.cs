using Microsoft.AspNetCore.Identity;
using NetCoreIdentity.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Areas.Identity
{
    public static class DefaultUserInitializer
    {
        public static void SeedData(UserManager<NetCoreIdentityUser> userManager)
        {
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<NetCoreIdentityUser> userManager)
        {
            var adminUser = userManager.FindByEmailAsync("crusade771022@hotmail.com").Result;
            if (adminUser != null && string.IsNullOrEmpty(adminUser.PasswordHash))
            {

                var token = userManager.GeneratePasswordResetTokenAsync(adminUser).Result;
                var result = userManager.ResetPasswordAsync(adminUser, token, "@Island0290").Result;
                if (result.Succeeded)
                {

                }
 
            }
        }
    }
}
