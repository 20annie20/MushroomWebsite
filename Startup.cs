using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MushroomWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.Logging;
using MushroomWebsite.Models;
using MushroomWebsite.Repository;
using MushroomWebsite.Repository.IRepository;
using Serilog;
using Serilog.Sinks.MSSqlServer;

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
            services.AddRazorPages();

            services.AddControllers();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            
        }
    }
}
