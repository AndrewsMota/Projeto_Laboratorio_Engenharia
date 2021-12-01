using App.Configurations;
using Business.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration);
            services.AddIdentity();
            services.AddAutoMapper(typeof(Startup));
            services.AddDependencias();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            CriarRolesECadastrarAdmin(serviceProvider);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void CriarRolesECadastrarAdmin(IServiceProvider serviceProvider)
        {
            string[] roles = { "Admin", "Presidente", "Pesquisador", "Secretária" };
            string email = "admin@admin.com";
            var senha = "Admin@123";

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var role in roles)
            {
                var roleExiste = roleManager.RoleExistsAsync(role).Result;

                if (!roleExiste)
                {
                    var roleResult = roleManager.CreateAsync(new IdentityRole(role));
                    roleResult.GetAwaiter().GetResult();
                }
            }

            var administrador = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var administradorCadastrado = userManager.FindByEmailAsync(email).Result;

            if (administradorCadastrado == null)
            {
                var criarAdministrador = userManager.CreateAsync(administrador, senha);
                criarAdministrador.GetAwaiter().GetResult();

                if (criarAdministrador.IsCompletedSuccessfully)
                {
                    userManager.AddToRoleAsync(administrador, "Admin").GetAwaiter().GetResult();
                }
            }
        }
    }
}
