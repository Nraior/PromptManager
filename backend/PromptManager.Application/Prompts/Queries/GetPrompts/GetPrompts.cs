using MediatR;
using Microsoft.EntityFrameworkCore;
using PromptManager.Application.Common.Interfaces;
using PromptManager.Application.Prompts.DTOs;

namespace PromptManager.Application.Prompts.Queries.GetPrompts
{
    public record GetPromptsQuery : IRequest<IEnumerable<PromptDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPromptsQueryHandler : IRequestHandler<GetPromptsQuery, IEnumerable<PromptDto>> 
    {
        private IPromptManagerDbContext _context;

        public GetPromptsQueryHandler(IPromptManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromptDto>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
        {
            var skipAmount = (request.PageNumber - 1) * request.PageSize;

            return await _context.Prompts.AsNoTracking() 
                .OrderByDescending(p => p.DateAsked)
                .Skip(skipAmount)
                .Take(request.PageSize)
                .Select(p => new PromptDto
                {
                    Id = p.Id,
                    DateAsked = p.DateAsked,
                    Text = p.Text,
                    Status = p.Status,
                    Response = p.Response
                })
                .ToListAsync(cancellationToken);
        }
    }
}
