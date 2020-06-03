using Dailylight.Server.Core;
using Dna;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Dailylight.Web.Server.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<DevotionReviewPageViewModel>();

            // Add controllers and views
            services.AddControllersWithViews();

            // Add token authentication state provider
            services.AddTokenAuthenticationStateProvider();

            // Add the context for our application
            services.AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(Framework.Construction.Configuration.GetConnectionString("DefaultConnection"), 
                    connection => connection.MigrationsAssembly("Dailylight.Web.Server.Admin")));

            // Add the identity service for our app
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity Options
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });

            // Configure cookie policy options
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            // Add Jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // Use authentication
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            // Use authorization
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            // Make sure we have the database
            serviceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
