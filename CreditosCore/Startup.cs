using CreditosCore.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CreditosCore.Shared;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Globalization;

namespace CreditosCore
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
            
            services.AddControllers();

            DataShared.ConfigIssuer = Configuration["Jwt:Issuer"];
            DataShared.KeyJWT = Configuration["Jwt:Key"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = DataShared.ConfigIssuer,
                    ValidAudience = DataShared.ConfigIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DataShared.KeyJWT))
                };
            });
            AddSwagger(services);

            services.AddDbContext<SqlDataContext>();
            
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                //SwaggerDoc("v1", new Info { title = "My API", version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SqlDataContext dataContext)
        {

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente API V1");

            });

            app.UseDeveloperExceptionPage();

            

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //add context for migration init
            SqlDataContext.fileNameDatabase = Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process);
            dataContext.Database.Migrate();

            //set Spanish, for use at Humanizer
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("es-ES");
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;


        }
    }
}
