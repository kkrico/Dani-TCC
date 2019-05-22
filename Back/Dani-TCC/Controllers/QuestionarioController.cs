using Dani_TCC.Core.EventHandlers;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dani_TCC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionarioController: ApiControllerBase

    {
        private readonly IQuestionarioService _questionarioService;

        public QuestionarioController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IQuestionarioService questionarioService) : base(notifications, mediator)
        {
            _questionarioService = questionarioService;
        }

        [HttpPost]
        public IActionResult IniciarQuestionario(RegisterQuestionarioViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            _questionarioService.RegisterQuestionario(model);

            return Response();
        }
    }
}