using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.Models.Algorithm;
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
            IMediatorHandler mediator, IEnumService enumService, IPhotoService photoService) : base(notifications, mediator)
        {
            _enumService = enumService;
        }

        [HttpGet]
        [Route("ethnicity")]
        public IActionResult GetEtnia()
        {
            return Response(_enumService.GetAll<Ethnicity>());
        }
        
        [HttpGet]
        [Route("gender")]
        public IActionResult GetGender()
        {
            return Response(_enumService.GetAll<Gender>());
        }
        
        [HttpGet]
        [Route("agegroup")]
        public IActionResult GetAgeGroup()
        {
            return Response(_enumService.GetAll<AgeGroup>());
        }
        
        [HttpGet]
        [Route("familyincome")]
        public IActionResult GetFamilyIncome()
        {
            return Response(_enumService.GetAll<FamilyIncome>());
        }
        
        [HttpGet]
        [Route("sexuality")]
        public IActionResult GetSexuality()
        {
            return Response(_enumService.GetAll<Sexuality>());
        }
    }
}