using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetMVCBaslangic.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetMVCBaslangic
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // context`e ihtiyaci olan veritabani adresini tanimlayin
            //Sql Localdb kullanicilari icin
            services.AddDbContext<DotnetMVCBaslangicDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DotnetMVCBaslangicDbConnStr")));

            // ef core 
            //  

            // SQLite kullanicilari icin
            //services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // ayca
            //using (var client = new DatabaseContext())
            //{
            //    client.Database.EnsureCreated();
            //}
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
