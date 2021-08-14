using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Btcsignal.Core.Services;
using Btcsignal.Infrastructures.Repositories;
using Btcsignal.Core.Models;
using Btcsignal.Core.Inerfaces.Services;
using Btcsignal.Core.Inerfaces.Repositories;
using Quartz.Spi;
using Quartz;
using WorkerDemoService.Jobs;
using Quartz.Impl;
using WorkerDemoService.JobFactory;
using WorkerDemoService.Models;
using System;
using WorkerDemoService.Schedular;
using BtcSignal.Api.Sheduler.Jobs;

namespace btcsignal_webservice
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
            services.AddCors();
            services.AddDbContext<BtcSignalDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("AlertTestDatabase")));

            services.AddIdentity<IdentityUser, IdentityRole>(
            options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }
            ).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BtcSignalDbContext>()
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

            /*services.InjectServices();
            services.InjectRepositories();*/

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddTransient<IMailService, SendGridMailService>();
            //services.AddControllersWithViews();
            //services.AddRazorPages();

            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Create a "key" for the job
                var jobKey = new JobKey("HelloWorldJob");

                // Register the job with the DI container
                q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the HelloWorldJob
                    .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                    .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds

            });

            // Add the Quartz.NET hosted service

            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);

            /*
                        services.AddSingleton<IJobFactory, MyJobFactory>();
                        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                        services.AddSingleton<QuartzJobRunner>();
                        services.AddSingleton<NotificationJob>();

                        services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0/10 * * * * ?"));

                        services.AddHostedService<MySchedular>();*/


            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(
            options => options.WithOrigins("http://localhost:3000", "http://localhost:8081").AllowAnyMethod().AllowAnyHeader()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
}
