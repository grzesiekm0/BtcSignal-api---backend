using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using btcsignalwebservice.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using btcsignalwebservice.Services;
using Btcsignal.Core.Services;
using Btcsignal.Core.Models;

namespace btcsignal_webservice
{
    public class Startup
    {
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            // services.AddDbContext<AlertContext>(opt =>
            //  opt.UseInMemoryDatabase("AlertList"));
            //services.AddDbContext<AlertContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AlertTestDatabase")));
            //services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AlertTestDatabase")));
            services.AddDbContext<BtcSignalDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("AlertTestDatabase")));
            services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AlertTestDatabase")));

            services.AddIdentity<IdentityUser, IdentityRole>(
            options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }
            ).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });

            
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IMailService, SendGridMailService>();
            //services.AddControllersWithViews();
            //services.AddRazorPages();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(enpoints =>
            {
                enpoints.MapControllers();
            });
            */

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            /*app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials*/
            app.UseCors(
            options => options.WithOrigins("http://localhost:3000", "http://localhost:8081").AllowAnyMethod().AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               // endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
    
}
