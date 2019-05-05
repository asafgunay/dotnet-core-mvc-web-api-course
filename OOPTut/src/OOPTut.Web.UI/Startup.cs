using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OOPTut.Application;
using OOPTut.Core.Users;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            // Identity template ile gelen login/register template ini kapatmak icin yorum satiri yap
            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationUserDbContext>();

            // Bizim olusturdugumuz template'i ekle
            services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationUserDbContext>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddScoped<IBazaarListService, BazaarListService>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
