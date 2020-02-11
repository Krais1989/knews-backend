using System;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using KNews.Identity.Entities;
using KNews.Identity.Persistence;
using KNews.Identity.Services;
using KNews.Identity.Services.Accounts.Handlers;
using KNews.Identity.Services.Accounts.Validators;
using KNews.Identity.Services.Services;
using KNews.Identity.WebApi.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KNews.Identity.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureIdentityServices(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IdentityContext>(db => { db.UseInMemoryDatabase("db_test_identity"); });

            var confSection = config.GetSection("IdentityOptions");
            services.Configure<IdentityOptions>(confSection);
            var idOpts = confSection.Get<IdentityOptions>();
            services.AddIdentityCore<User>(opt =>
                {
                    Configuration.Bind("IdentityOptions", opt);
                    // opt.User = idOpts.User;
                    // opt.Password = idOpts.Password;
                    // opt.SignIn = idOpts.SignIn;
                    // opt.Lockout = idOpts.Lockout;
                })
                .AddRoles<Role>()
                .AddUserStore<UserStore<User, Role, IdentityContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>>() // required
                .AddRoleStore<RoleStore<Role, IdentityContext, int, UserRole, RoleClaim>>() // required
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, Role>>()
                .AddUserManager<IdentityUserManager>()
                .AddRoleManager<IdentityRoleManager>()
                .AddSignInManager<IdentitySignInManager>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddSingleton<IJWTFactory, JwtFactory>();
        }

        private void ConfigureAuthentication(IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();

            /* Валидация JWT токена */
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtSettings.GetSymmetricSecurityKey(),

                    ValidateAudience = false,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = false,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateLifetime = true,
                };
            });
        }

        private void ConfigureMediatrServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(AccountChangePasswordRequestHandler).Assembly);
        }
        
        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KNews Identity Api",
                    Version = "v1",
                    Description = "Swagger UI for KNews Identity Web API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Egor Krais",
                        Email = "krais1989@gmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthentication(services, Configuration);
            ConfigureIdentityServices(services, Configuration);
            ConfigureMediatrServices(services);
            ConfigureSwaggerServices(services);

            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(typeof(AccountChangePasswordValidator).Assembly);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KNews Api v1");
            });

            app.UseHttpsRedirection();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
