using System;
using BusTicket.Data;
using ContactManager.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BusTicket.Areas.Identity.IdentityHostingStartup))]
namespace BusTicket.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BusTicketDataContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BusTicketContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BusTicketDataContext>();
                services.AddRazorPages();

               /* services.AddAuthorization(options =>
                {
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                });*/
                services.AddControllers(config =>
                {
                    // using Microsoft.AspNetCore.Mvc.Authorization;
                    // using Microsoft.AspNetCore.Authorization;
                    var policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                });

                services.AddScoped<IAuthorizationHandler,
                         ContactIsOwnerAuthorizationHandler>();

                services.AddSingleton<IAuthorizationHandler,
                                      ContactAdministratorsAuthorizationHandler>();

                services.AddSingleton<IAuthorizationHandler,
                                      ContactManagerAuthorizationHandler>();
            });
        }
    }
}