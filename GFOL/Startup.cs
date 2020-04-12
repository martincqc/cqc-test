using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFOL.AddressFinder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using GFOL.Data;
using GFOL.Helpers;
using GFOL.Models;
using GFOL.Repository;
using GFOL.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApplicationDbContext = GFOL.Data.ApplicationDbContext;

namespace GFOL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession(so =>
            {
                so.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddScoped<IGenericRepository<Schema>, SchemaRepository>();
            services.AddScoped<IGenericRepository<Submission>, SubmissionRepository>();
            services.AddScoped<IEntity, Schema>();
            services.AddScoped<IEntity, Submission>();
            services.AddScoped<ISubmissionService, SubmissionService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IGFOLValidation, GFOLValidation>();
            services.AddScoped<IAddressFinder, AddressFinder.AddressFinder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
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
    }
}
