using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocketElevatorsCustomerPortal.Areas.Identity.Data;
using RocketElevatorsCustomerPortal.Data;

[assembly: HostingStartup(typeof(RocketElevatorsCustomerPortal.Areas.Identity.IdentityHostingStartup))]
namespace RocketElevatorsCustomerPortal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                string dbstring = context.Configuration.GetConnectionString("DefaultConnection");
                var serverVersion = new MySqlServerVersion(new Version(5, 0));
                services.AddDbContext<RocketElevatorsCustomerPortalContext>(opt =>
                        opt.UseMySql(dbstring, serverVersion));
                services.AddDefaultIdentity<RocketElevatorsCustomerPortalUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<RocketElevatorsCustomerPortalContext>();
            });
        }
    }
}