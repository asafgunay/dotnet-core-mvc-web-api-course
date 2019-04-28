using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OOPTut.EntityFramework.Contexts;

namespace OOPTut.Web.UI
{
    public class Startup
    {

        // appsettings.json yani Configuration'a erisebilmek icin yapici metod olusturuyorum.
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Uygulama calismaya baslamadan once ve calisma sirasinda gerekli olan *servislerin* belirli standartlara gore cagrildigi yer
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }


        // uygulamanin derlendigi anda http isteklere nasil cevap verileceginin ve uygulamanin hangi standartlar ile calisaginin belirtildigi yer!
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // statik dosyalarin kullanimini acar herkese!
            // Ayrica new StaticFileOptions() ile wwwroot disinda kalan node_modules klasorunu /vendor path'i ile cagirmamizi ayarliyoruz.
            //app.UseStaticFiles(new StaticFileOptions() {
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
            //    RequestPath = new PathString("/vendor")
            //});

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
