using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StudentDataService.Config;
using StudentDataService.Contracts.Attributes;

namespace StudentDataService
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

            services.AddCors();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                //options.JsonSerializerOptions.DictionaryKeyPolicy = null;//��� CamelCase
                //options.JsonSerializerOptions.PropertyNamingPolicy = null;//��� CamelCase
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });

            services.AddSwaggerGen();

            services.Configure<AuthConfig>(Configuration.GetSection("Jwt"));
            var connectionStr = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<Entity.Context.IEntityContext, Entity.Context.EntityContext>(options => options.UseSqlite(connectionStr));
            services.AddScoped<Entity.Repository.Group.IGroupRepository, Entity.Repository.Group.GroupRepository>();
            services.AddScoped<Entity.Repository.Student.IStudentRepository, Entity.Repository.Student.StudentRepository>();
            services.AddScoped<Entity.Repository.Student.IStudentToGroupRepository, Entity.Repository.Student.StudentToGroupRepository>();
            services.AddScoped<Entity.Repository.User.IUserRepository, Entity.Repository.User.UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
        }
    }
}
