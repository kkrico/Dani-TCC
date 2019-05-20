using Autofac;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Model;
using Dani_TCC.Core.Service;
using MediatR;

namespace Dani_TCC
{
    public class DefaultModule : Module
    {
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

            builder.RegisterType<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Dani_TCC.Core.Service.QuestaoService).Assembly)
                .PublicOnly()
                .Where(e => e.Name.EndsWith("Service"))
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
            
            builder.RegisterType<QuestaoService>().As<IQuestaoService>();
            builder.RegisterType<InMemoryMediatorHandler>().As<IMediatorHandler>();
            builder.RegisterType<DomainNotificationHandler>().As<INotificationHandler<DomainNotification>>();
        }
    }
}