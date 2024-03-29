using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Sinks.MSSqlServer;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using System.Text;

using MushroomWebsite.Data;
using MushroomWebsite.Models;
using MushroomWebsite.Repository;
using MushroomWebsite.Repository.IRepository;
using Microsoft.AspNetCore.Http;

namespace MushroomWebsite
{
    public class Startup
    {

        private const string _connectionStringName = "DefaultConnection";
        private const string _schemaName = "dbo";
        private const string _tableName = "LogEvents";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddRazorPages(options=>
            {
                options.Conventions.AuthorizeAreaFolder("User", "/");
                options.Conventions.AuthorizeAreaFolder("Admin", "/");
                options.Conventions.AllowAnonymousToFolder("/");
            });

            services.AddSession();
            
            services.AddControllersWithViews();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var columnOptionsSection = Configuration.GetSection("Serilog:ColumnOptions");
            var sinkOptionsSection = Configuration.GetSection("Serilog:SinkOptions");

            Log.Logger = new LoggerConfiguration()
               .WriteTo.MSSqlServer(
                   connectionString: _connectionStringName,
                   sinkOptions: new MSSqlServerSinkOptions
                   {
                       TableName = _tableName,
                       SchemaName = _schemaName,
                       AutoCreateSqlTable = true
                   },
                   sinkOptionsSection: sinkOptionsSection,
                   appConfiguration: Configuration,
                   columnOptionsSection: columnOptionsSection)
               .CreateLogger();

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

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            
        }
    }
}
