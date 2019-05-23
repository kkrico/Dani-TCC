using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Dani_TCC.Configurations;
using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.Models.Algorithm;
using Dani_TCC.Core.Models.Enums;
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
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.Resolve(t);
            });
            
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            

            builder.RegisterType<DB_PESQUISA_TCCContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .PublicOnly()
                .Where(e => e.Name.EndsWith(Service))
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .PublicOnly()
                .Where(e => e.Name.EndsWith(Handler))
                .AsImplementedInterfaces();

            builder.RegisterType<PatternFileSearchAlgorithmFileSearchAlgorithm>().As<IPatternFileSearchAlgorithm>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("pattern", "jpg"), 
                    new NamedParameter("parseType", ParseType.Relative), 
                });
            builder.RegisterType<PhotoFileParserAlgorithm>().As<IFileParserAlgorithm<Photo>>();
            builder.RegisterType<EntitySearchAlgorithmAlgorithm<Photo>>().As<IEntitySearchAlgorithm<Photo>>();
        }
    }
}