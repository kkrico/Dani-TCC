using System.Threading.Tasks;
using Dani_TCC.Core.Events;
using MediatR;

namespace Dani_TCC.Core.EventHandlers
{
    public sealed class InMemoryMediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryMediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}