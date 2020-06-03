using Dailylight.Server.Core;
using Dna;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PayStack.Net;
using System.Text;

namespace Dailylight.Web.Server
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
            // Add controller and views
            services.AddControllersWithViews();

            // Add SendGrid email sender
            services.AddSendGridEmailSender();

            // Add general email template sender
            services.AddEmailTemplateSender();

            // Add database context
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Framework.Construction.Configuration.GetConnectionString("DefaultConnection")));

            // Add identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add JWT Authentication for Api clients
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    // Set validation parameters
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate issuer
                        ValidateIssuer = true,
                        // Validate audience
                        ValidateAudience = true,
                        // Validate expiration
                        ValidateLifetime = true,
                        // Validate signature
                        ValidateIssuerSigningKey = true,

                        // Set issuer
                        ValidIssuer = Framework.Construction.Configuration["Jwt:Issuer"],
                        // Set audience
                        ValidAudience = Framework.Construction.Configuration["Jwt:Audience"],

                        // Set signing key
                        IssuerSigningKey = new SymmetricSecurityKey(
                            // Get our secret key from configuration
                            Encoding.UTF8.GetBytes(Framework.Construction.Configuration["Jwt:SecretKey"])),
                    };
                });

            // Change password policy
            services.Configure<IdentityOptions>(options =>
            {
                // Make really weak passwords possible
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // Make sure users have unique emails
                options.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use Dna Framework
            app.UseDnaFramework();

            // Setup Identity
            app.UseAuthentication();

            // If in development...
            if (env.IsDevelopment())
            {
                // Show any exceptions in browser when they crash
                app.UseDeveloperExceptionPage();
            }
            // Otherwise...
            else
            {
                // Just show generic error page
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Redirect all calls from HTTP to HTTPS
            app.UseHttpsRedirection();

            // Serve static files
            app.UseStaticFiles();

            // Use routing
            app.UseRouting();

            // Use authorization
            app.UseAuthorization();

            // Configure endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
