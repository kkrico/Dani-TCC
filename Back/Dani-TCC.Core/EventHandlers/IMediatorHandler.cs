using System.Threading.Tasks;
using Dani_TCC.Core.Events;

namespace Dani_TCC.Core.EventHandlers
{
    public interface IMediatorHandler
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}