using Domain.Handlers;
using Domain.Models;
using MediatR;
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

        public void ConfigureServices(IServiceCollection services)
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

            services.AddSingleton(_ => Configuration.Get<Common.Configuration>());

            services.AddDbContext<DatabaseHandler.DatabaseDbContext>();

            services.AddTransient<IEntityHandler<Author>, DatabaseHandler.EntityHandler<Author>>();
            services.AddTransient<IEntityHandler<Book>, DatabaseHandler.EntityHandler<Book>>();

            services.AddTransient<AuthorService.AuthorService>();
            services.AddTransient<BookService.BookService>();

            services.AddMediatR(typeof(AuthorService.AuthorService),
                typeof(BookService.BookService));
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseCors("Access-Control-Allow-Origin");
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}