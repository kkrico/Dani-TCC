using System;
using System.Threading;
using System.Threading.Tasks;
using Dani_TCC.Configurations;
using Dani_TCC.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dani_TCC.Filters
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // Instanciated over reflection
    public class PhotoScanHostedService: IHostedService
    {
        // We need to inject the IServiceProvider so we can create 
        // the scoped service, MyDbContext
        private readonly IServiceProvider _serviceProvider;

        public PhotoScanHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using(IServiceScope scope = _serviceProvider.CreateScope())
            {
                var settings = scope.ServiceProvider.GetService<PhotoScanSettings>();
                var service = scope.ServiceProvider.GetService<IPhotoService>();
                
                if (service != null && settings != null)
                    service.ParsePhotos(settings.Directory);
            }
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // noop
            return Task.CompletedTask;
        }
    }


}