using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Identify.Application;
using Identify.Application.Password;
using Identify.Infrastructure.IdentityDb;
using System.Reflection;
using MediatR;
using Identify.Infrastructure;
using Identify.Application.IdentityServer;
using Identify.Application.Commands.Users;

namespace Identify.Web
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
            services.AddMediatR(typeof(UserCreateCommand).Assembly);
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();
            services.AddHttpContextAccessor();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<IHashingService, Pbkdf2HashingService_HMACSHA256_1000>();
            services.AddTransient<IHashingServiceFactory, HashingServiceFactory>();
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:IdentityDb"]));
            services.AddScoped<IIdentityDbContext>(serviceProvider => serviceProvider.GetRequiredService<IdentityDbContext>());
            services.Configure<IdentityServerOptions>(Configuration.GetSection("IdentityServerOptions"));
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));
            services.AddTransient<IEmailService, SendGridEmailService>();
            services.AddCors();

            //We'll keep all migrations in the Identify.Infrastructure assembly
            var migrationsAssembly = typeof(IdentityDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/login";
                options.UserInteraction.ConsentUrl = "/consent";
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(Configuration["ConnectionStrings:IdentityDb"],
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options => {
                options.ConfigureDbContext = b => b.UseSqlServer(Configuration["ConnectionStrings:IdentityDb"],
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>()
            .AddJwtBearerClientAuthentication()
            .AddDeveloperSigningCredential();


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "client-app/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(options => {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identify API");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
