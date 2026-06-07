using Microsoft.Extensions.AI;
using PromptManager.Application.Common.Interfaces;

namespace PromptManager.Infrastructure.Services
{
    public class OllamaChatService : IChatService
    {
        private readonly IChatClient _client;
        public OllamaChatService(IChatClient client)
        {
            _client = client;
        }

        public async Task<string> GetResponseAsync(string prompt, CancellationToken ct)
        {
            var response = await _client.GetResponseAsync(prompt, cancellationToken: ct);
            return response.Messages.LastOrDefault()?.Text ?? string.Empty;
        }
    }
}
