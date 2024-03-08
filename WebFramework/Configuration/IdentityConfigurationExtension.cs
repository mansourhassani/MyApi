using Common;
using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Configuration
{
    public static class IdentityConfigurationExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(IdentityOptions =>
            {
                //Password Settings
                IdentityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                IdentityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                IdentityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric; //#@!
                IdentityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                IdentityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                //UserName Settings
                IdentityOptions.User.RequireUniqueEmail = settings.UserRequireUniqueEmail;

                //Sigin Settings
                //IdentityOptions.SignIn.RequireConfirmedEmail = false;
                //IdentityOptions.SignIn.RequireConfirmedPhoneNumber = false;

                //Lockout Settings
                //IdentityOptions.Lockout.MaxFailedAccessAttempts = 5;
                //IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //IdentityOptions.Lockout.AllowedForNewUsers = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
