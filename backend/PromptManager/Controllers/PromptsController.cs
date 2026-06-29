using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromptManager.Application.Prompts.Commands.CreatePrompt;
using PromptManager.Application.Prompts.Commands.ProcessPrompt;
using PromptManager.Application.Prompts.Queries.GetPrompts;

namespace PromptManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromptsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrompts([FromQuery] GetPromptsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> CreatePrompt([FromBody] CreatePromptCommand command)
        {
            var promptId = await _mediator.Send(command);
            BackgroundJob.Enqueue<IMediator>(m =>
                m.Send(new ProcessPromptCommand(promptId), CancellationToken.None));

            return StatusCode(StatusCodes.Status201Created, promptId);
        }
    }
}
