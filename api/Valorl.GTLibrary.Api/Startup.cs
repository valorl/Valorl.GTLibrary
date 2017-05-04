using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Valorl.GTLibrary.DataAccess;
using Valorl.GTLibrary.DataAccess.Interfaces;

namespace Valorl.GTLibrary.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);

            var builder = new ContainerBuilder();
            RegisterRepository<ItemRepository, IItemRepository>(builder);
            RegisterRepository<ItemCopyRepository, IItemCopyRepository>(builder);
            RegisterRepository<LibraryRepository, ILibraryRepository>(builder);
            RegisterRepository<AcquirementRepository, IAcquirementRepository>(builder);
            RegisterRepository<MemberRepository, IMemberRepository>(builder);
            RegisterRepository<LibraryRepository, ILibraryRepository>(builder);

            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }

        private void RegisterRepository<TImplementation, TInterface>(ContainerBuilder builder)
        {
            builder.RegisterType<TImplementation>()
                .WithParameter("connectionString", Configuration.GetConnectionString("GTLibrary"))
                .As<TInterface>()
                .InstancePerLifetimeScope();
        }
    }
}
