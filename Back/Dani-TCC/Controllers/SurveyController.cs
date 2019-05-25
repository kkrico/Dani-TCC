using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.Services;
using Dani_TCC.Core.ViewModels;
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
            return Response(_surveyService.RegisterSurvey(model));
        }
    }
}