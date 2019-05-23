using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.Models.Enums;
using Dani_TCC.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dani_TCC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SocioEconomicController : ApiControllerBase 
    {
        private readonly IEnumService _enumService;

        public SocioEconomicController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator, IEnumService enumService) : base(notifications, mediator)
        {
            _enumService = enumService;
        }

        [HttpGet]
        [Route("etnia")]
        public IActionResult GetEtnia()
        {
            return Response(_enumService.GetAll<Ethnicity>());
        }
        
        [HttpGet]
        [Route("genero")]
        public IActionResult GetGenero()
        {
            return Response(_enumService.GetAll<Gender>());
        }
        
        [HttpGet]
        [Route("faixaetaria")]
        public IActionResult GetFaixaEtaria()
        {
            return Response(_enumService.GetAll<AgeGroup>());
        }
        
        [HttpGet]
        [Route("rendafamiliar")]
        public IActionResult GetRendaFamiliar()
        {
            return Response(_enumService.GetAll<RendaFamiliar>());
        }
        
        [HttpGet]
        [Route("sexualidade")]
        public IActionResult GetSexualidade()
        {
            return Response(_enumService.GetAll<Sexualidade>());
        }
    }
}