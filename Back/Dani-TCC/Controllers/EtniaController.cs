using Dani_TCC.Core.Events;
using Dani_TCC.Core.Model;
using Dani_TCC.Core.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dani_TCC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtniaController : ApiControllerBase 
    {
        private readonly IEnumService _enumService;

        public EtniaController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator, IEnumService enumService) : base(notifications, mediator)
        {
            _enumService = enumService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response(_enumService.GetAll<Etnia>());
        }
    }
}