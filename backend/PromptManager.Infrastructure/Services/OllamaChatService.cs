using Microsoft.Extensions.AI;
using Polly;
using Polly.Registry;
using PromptManager.Application.Common.Interfaces;

namespace PromptManager.Infrastructure.Services
{
    public class OllamaChatService : IChatService
    {
        public const string ResiliencePipelineName = "ollama";

        private readonly IChatClient _client;
        private readonly ResiliencePipeline _pipeline;

        public OllamaChatService(IChatClient client, ResiliencePipelineProvider<string> pipelineProvider)
        {
            _client = client;
            _pipeline = pipelineProvider.GetPipeline(ResiliencePipelineName);
        }

        public async Task<string> GetResponseAsync(string prompt, CancellationToken ct)
        {
            var response = await _pipeline.ExecuteAsync(
                async cancellationToken => await _client.GetResponseAsync(prompt, cancellationToken: cancellationToken),
                ct);
            return response.Messages.LastOrDefault()?.Text ?? string.Empty;
        }
    }
}
