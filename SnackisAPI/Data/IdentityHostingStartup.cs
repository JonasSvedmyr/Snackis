using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnackisAPI.Models;


[assembly: HostingStartup(typeof(SnackisAPI.Data.IdentityHostingStartup))]
namespace SnackisAPI.Data
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ContextConnection")));


                IdentityBuilder identityBuilder = services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<Context>()
                    .AddSignInManager<SignInManager<User>>();
            });
        }
    }
}
