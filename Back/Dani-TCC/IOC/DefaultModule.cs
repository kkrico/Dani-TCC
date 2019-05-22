using Autofac;
using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Services;
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

            builder.RegisterAssemblyTypes(typeof(QuestionarioService).Assembly)
                .PublicOnly()
                .Where(e => e.Name.EndsWith("Service"))
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
            
            builder.RegisterType<QuestionarioService>().As<IQuestionarioService>();
            builder.RegisterType<InMemoryMediatorHandler>().As<IMediatorHandler>();
            builder.RegisterType<DomainNotificationHandler>().As<INotificationHandler<DomainNotification>>();
        }
    }
}