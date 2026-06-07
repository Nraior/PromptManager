using MediatR;
using Microsoft.Extensions.Options;
using PromptManager.Application.Ai.DTOs;
using PromptManager.Application.Common.Options;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace PromptManager.Application.Ai.Queries.GetAiConfiguration
{
    public record GetAiConfigurationQuery : IRequest<AiConfigDto>;

    public class GetAiConfigurationHandler : IRequestHandler<GetAiConfigurationQuery, AiConfigDto>
    {
        AiSettings _options;
        public GetAiConfigurationHandler(IOptions<AiSettings> options) 
        {
            _options = options.Value;
        }
        public Task<AiConfigDto> Handle(GetAiConfigurationQuery request, CancellationToken cancellationToken)
        {
            var model = _options.ActiveProvider switch
            {
                "Ollama" => _options.Ollama.Model,
                _ => "Unknown"
            };

            return Task.FromResult(new AiConfigDto(_options.ActiveProvider, model));
        }
    }
}
