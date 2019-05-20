using System.Threading.Tasks;
using MediatR;

namespace Dani_TCC.Core.Events
{
    public interface IMediatorHandlerNormalize
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
    
    public sealed class InMemoryBusNormalize : IMediatorHandlerNormalize
    {
        private readonly IMediator _mediator;

        public InMemoryBusNormalize(IMediator mediator)
        {_mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}