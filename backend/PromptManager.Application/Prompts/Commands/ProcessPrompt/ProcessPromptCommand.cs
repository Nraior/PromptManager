using MediatR;
using PromptManager.Application.Common.Interfaces;
using PromptManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PromptManager.Application.Common.Exceptions; // 👈 to jest fix - daje FirstOrDefaultAsync

namespace PromptManager.Application.Prompts.Commands.ProcessPrompt
{
    public record ProcessPromptCommand(Guid PromptId) : IRequest;

    public class ProcessPromptCommandHandler : IRequestHandler<ProcessPromptCommand>
    {
        private readonly IPromptManagerDbContext _context;
        private readonly IChatService _chatService;

        public ProcessPromptCommandHandler(IPromptManagerDbContext context, IChatService chatService)
        {
            _context = context;
            _chatService = chatService;
        }

        public async Task Handle(ProcessPromptCommand request, CancellationToken cancellationToken)
        {
            var prompt = await _context.Prompts
                .FirstOrDefaultAsync(p => p.Id == request.PromptId, cancellationToken)
                ?? throw new NotFoundException(nameof(Prompt), request.PromptId);

            prompt.SetProcessing();
            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                var response = await _chatService.GetResponseAsync(prompt.Text, cancellationToken);
                prompt.SetSuccessful(response);
                await _context.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                prompt.SetError(ex.Message);
                await _context.SaveChangesAsync(cancellationToken);
                throw;

            }


        }
    }
}
