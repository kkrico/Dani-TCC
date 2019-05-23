using System.Reflection;
using Autofac;
using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Services;
using Dani_TCC.Filters;
using MediatR;
using Module = Autofac.Module;

namespace Dani_TCC
{
    public class DefaultModule : Module
    {
        private const string Handler ="Handler";
        private const string Service = "Service";
        private const string Algorithm = "Algorithm";
        
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.Resolve(t);
            });

            Assembly coreAssembly = typeof(SurveyService).Assembly;

            builder.RegisterAssemblyTypes(coreAssembly)
                .PublicOnly()
                .Where(e => e.Name.EndsWith(Service))
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(coreAssembly)
                .PublicOnly()
                .Where(e => e.Name.EndsWith(Algorithm))
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();


            builder.RegisterAssemblyTypes(coreAssembly)
                .PublicOnly()
                .Where(e => e.Name.EndsWith(Handler))
                .AsImplementedInterfaces();

        }
    }
}