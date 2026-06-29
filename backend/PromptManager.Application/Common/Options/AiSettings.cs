using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Application.Common.Options
{
    public class AiSettings
    {
        public string ActiveProvider { get; set; } = string.Empty;
        public OllamaSettings Ollama { get; set; } = new();
    }

    public class OllamaSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 60;
        public int MaxRetryAttempts { get; set; } = 2;
        public int RetryDelayMilliseconds { get; set; } = 500;
        public int CircuitBreakerFailureThreshold { get; set; } = 3;
        public int CircuitBreakerBreakSeconds { get; set; } = 30;
    }
}
