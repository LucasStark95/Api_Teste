using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NPista.API.Helpers;
using NPista.Data.EFCore.Context;
using NPista.Data.EFCore.Helpers;
using NPista.Data.EFCore.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NPista.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services = ConfigureServer(services);
            services = ConfigureData(services);
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("SiteCorsPolicy");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IServiceCollection ConfigureServer(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            return services;
        }

        private IServiceCollection ConfigureData(IServiceCollection services)
        {
            if (CurrentEnvironment.IsEnvironment("Test"))
                services.AddDbContext<Contexto>(options =>
                    options.UseInMemoryDatabase(databaseName: "NPista"));
            else
            {
                var connectionString = ConnectionHelper.GetConnectionString();
                services.AddDbContext<Contexto>(options => options.UseNpgsql(connectionString));
            }

            var dataServiceTypes = GetServicesTypes();
            foreach (var t in dataServiceTypes)
                services.AddScoped(t);
            
            return services;
        }

        private IEnumerable<Type> GetServicesTypes() => Assembly.GetAssembly(typeof(Contexto)).GetTypes()
                .Where(t => t != typeof(RepositorioBase<>) && InjectionHelper.IsSubclassOfRawGeneric(typeof(RepositorioBase<>), t));
    }
}
