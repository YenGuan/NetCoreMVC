﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreIdentity.Web.Areas.Identity.Data;


[assembly: HostingStartup(typeof(NetCoreIdentity.Web.Areas.Identity.IdentityHostingStartup))]
namespace NetCoreIdentity.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<NetCoreIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("NetCoreIdentityContextConnection")));

                services.AddDefaultIdentity<NetCoreIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<NetCoreIdentityRole>()
                    .AddEntityFrameworkStores<NetCoreIdentityContext>();
            });
           
        }
    }
}