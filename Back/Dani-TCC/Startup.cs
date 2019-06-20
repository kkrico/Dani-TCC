using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dani_TCC.Configurations;
using Dani_TCC.Core.Models;
using Dani_TCC.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Dani_TCC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }
        
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            PhotoScanSettings photoScanSettings = Configuration
                                                      .GetSection(nameof(PhotoScanSettings))
                                                      .Get<PhotoScanSettings>() ?? throw new Exception("Photo Settings null");

            if (HostingEnvironment.IsDevelopment() && photoScanSettings.ShouldScan)
            {
                services.AddScoped(c => photoScanSettings);
            }

            services.AddMemoryCache();

            services.AddDbContext<DB_PESQUISA_TCCContext>(options =>
            {
                    string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_DB_PESQUISA_TCC") ??
                                              Configuration.GetConnectionString("DB_PESQUISA_TCC");
                    Console.Out.WriteLine(connectionString);
                    options.UseMySQL(connectionString);
                });
            
            services.AddWebApi(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
            });

            
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TCC Dani",
                    Description = "API Swagger surface",
                    Contact = new Contact { Name = "Daniel Ramos", Email = "ramos.danielferreira@gmail.com", Url = "http://dframos.com" },
                });
            });
            
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();
            
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Pesquisa Psicologia, por Daniel Ramos e Daniel Ribeiro");
                });

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
           
            app.UseMvc();
        }
    }
}
