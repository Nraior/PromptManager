using FluentValidation;

namespace PromptManager.Application.Prompts.Queries.GetPrompts
{
    public class GetPromptsQueryValidator : AbstractValidator<GetPromptsQuery>
    {
        public GetPromptsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, GetPromptsQuery.MaxPageSize)
                .WithMessage($"PageSize must be between 1 and {GetPromptsQuery.MaxPageSize}");
        }
    }
}
