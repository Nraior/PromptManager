using MediatR;
using PromptManager.Application.Common.Interfaces;
using PromptManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Application.Prompts.Commands.CreatePrompt
{
    public record CreatePromptCommand : IRequest<Guid> {
        public string Text { get; init; } = String.Empty;
    }

    public class CreatePromptCommandHandler : IRequestHandler<CreatePromptCommand, Guid>
    {
        private readonly IPromptManagerDbContext _context;

        public CreatePromptCommandHandler(IPromptManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePromptCommand request, CancellationToken token) 
        {
            var entity = new Prompt(request.Text);

            _context.Prompts.Add(entity);

            await _context.SaveChangesAsync(token);

            return entity.Id;
        }
    }
}
