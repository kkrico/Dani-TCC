using Autofac;
using Dani_TCC.Core.Events;
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
            
            builder.RegisterType<QuestaoService>().As<IQuestaoService>();
            builder.RegisterType<InMemoryBusNormalize>().As<IMediatorHandlerNormalize>();
            builder.RegisterType<DomainNotificationHandler>().As<INotificationHandler<DomainNotification>>();
        }
    }
}