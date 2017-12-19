using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Api
{
    class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            Configuration = Builder.Build();
        }

        IConfigurationBuilder Builder { get; }
        IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Access-Control-Allow-Origin", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "API V1",
                        Version = "v1"
                    });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                c.IncludeXmlComments(xmlPath);

                c.IgnoreObsoleteProperties();
                c.DescribeAllEnumsAsStrings();
            });

            var container = ConfigureContainer(services);
            return new AutofacServiceProvider(container);
        }

        public IContainer ConfigureContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance<Common.IConfiguration>(new Configuration(Configuration));
            builder.RegisterModule<Core.CoreModule>();

            builder.Populate(services);

            return builder.Build();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                loggerFactory.AddDebug();

            app.UseMiddleware<Logging.ErrorHandlingMiddleware>();

            app.UseStaticFiles();

            app.UseCors("Access-Control-Allow-Origin");
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }

        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
    }
}