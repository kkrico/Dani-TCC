using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dani_TCC.Core.Events;
using Dani_TCC.Core.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dani_TCC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiControllerBase
    {
        private readonly IQuestaoService _questaoService;

        public ValuesController(INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediator,
            IQuestaoService questaoService) : base(notifications, mediator)
        {
            _questaoService = questaoService;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return Response(new{TotalQuestoes = _questaoService.QuantidadeQuestoes()});
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

       
    }
}
