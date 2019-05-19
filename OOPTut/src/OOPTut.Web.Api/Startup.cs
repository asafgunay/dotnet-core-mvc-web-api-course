using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OOPTut.Application;
using OOPTut.Application.BazaarListItemServices;
using OOPTut.Core.Users;
using OOPTut.EntityFramework.Contexts;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Text;

namespace OOPTut.Web.Api
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
            #region CORS
            // CORS  -- tarayıcı tabanlı güvenlik önlemi

            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            #endregion

            #region DbContext
            services.AddDbContext<ApplicationUserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
            #endregion

            #region Identity & JWT Configuration

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserDbContext>();



            // Add Authentication -- Auth/Token Yonetimi

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = true,
                    ValidAudience = this.Configuration["Tokens:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = this.Configuration["Tokens:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            #endregion

            #region OOPTut.Application --> Services
            services.AddScoped<IBazaarListService, BazaarListService>();

            services.AddScoped<IBazaarListItemService, BazaarListItemService>();
            #endregion


            #region Swagger

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("OOPTutApi", new Info
                {
                    Title = "Pazar Listesi Uygulamasi API Dokumantasyonu",
                    Version = "0.0.1",
                    Contact = new Contact
                    {
                        Email = "info@kodluyoruz.org",
                        Name = "Bilgin Kahraman",
                        Url = "kodluyoruz.org"
                    },
                    Description = "Pazar Listesi uygulamasinin gerekli olan tum servislerini icerir",
                    TermsOfService = "kodluyoruz.org/privacy"
                });

                // JWT integration
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[]{ } },
                };

                s.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Type = "apiKey",
                    Description = "JWT Auth",
                    In = "header",
                    Name = "Authorization"
                });

                s.AddSecurityRequirement(security);

            });
            #endregion


            #region MVC Structure
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("Cors");
            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Swagger UI
            app.UseSwagger().UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("/swagger/OOPTutApi/swagger.json", "Swagger Test Api Endpoint");
            });
            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
