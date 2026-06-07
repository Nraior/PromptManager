using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace PromptManager.Application.Prompts.Commands.CreatePrompt
{
    public class CreatePromptCommandValidator : AbstractValidator<CreatePromptCommand>
    {
        public CreatePromptCommandValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Prompt cannot be empty")
                .MinimumLength(5).WithMessage("Prompt needs to be be at least 5 letters long")
                .MaximumLength(2000).WithMessage("Prompt length exceeded (Max: 2000 characters)");
        }
    }
}
