using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dani_TCC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SurveyController: ApiControllerBase

    {
        private readonly ISurveyService _surveyService;

        public SurveyController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            ISurveyService surveyService) : base(notifications, mediator)
        {
            _surveyService = surveyService;
        }

        [HttpPost]
        public IActionResult RegisterSurvey(RegisterSurveyViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            _surveyService.RegisterSurvey(model);

            return Response();
        }
    }
}