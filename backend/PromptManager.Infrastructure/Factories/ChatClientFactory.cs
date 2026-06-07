using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using PromptManager.Application.Common.Options;

namespace PromptManager.Infrastructure.Factories
{
    public static class ChatClientFactory
    {
        public static IChatClient Create(AiSettings settings)
        {
            return settings.ActiveProvider switch
            {
                "Ollama" => new OllamaChatClient(
                    new Uri(settings.Ollama.BaseUrl),
                    settings.Ollama.Model
                ),
                var unknown => throw new InvalidOperationException($"Unknown AI provider: {unknown}")
            };
        }

    }
}
