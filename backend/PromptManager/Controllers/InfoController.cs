using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromptManager.Application.Ai.Queries.GetAiConfiguration;

namespace PromptManager.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InfoController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        [HttpGet("Model")]
        public async Task<IActionResult> GetModel()
        {
            var result = await _mediator.Send(new GetAiConfigurationQuery());
            return Ok(result);

        }
    }
}
