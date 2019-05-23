using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dani_TCC.Configurations;
using Dani_TCC.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
                services.AddHostedService<PhotoScanHostedService>();
            }
            
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
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Modelo Arquitetura API v1.1");
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
           
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
