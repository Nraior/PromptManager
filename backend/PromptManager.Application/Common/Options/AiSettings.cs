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
    }
}
