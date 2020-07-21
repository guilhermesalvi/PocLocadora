using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PocLocadora.Data;
using PocLocadora.Models;
using System;
using System.Linq;

namespace PocLocadora
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PocLocadoraDbContext>(options =>
            {
                options.UseInMemoryDatabase("PocLocadora");
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            InitializeDatabase(app);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PocLocadoraDbContext>();

                if (context.Genero.FirstOrDefault(x => x.Nome == "Comédia") is null)
                {
                    context.Genero.Add(new Genero
                    {
                        Id = new Guid("85cba1cb-6b27-4a8e-b63e-e9c88d703e3d"),
                        Nome = "Comédia"
                    });
                }

                if (context.Genero.FirstOrDefault(x => x.Nome == "Ação") is null)
                {
                    context.Genero.Add(new Genero
                    {
                        Id = new Guid("bae48e45-7f5f-44cf-b87a-79810e568e35"),
                        Nome = "Ação"
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
