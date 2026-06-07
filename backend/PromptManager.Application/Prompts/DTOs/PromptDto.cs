using PromptManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Application.Prompts.DTOs
{
    public class PromptDto
    {
        public Guid Id { get; set; }
        public DateTime DateAsked { get; set; }
        public string Text { get; set; } = string.Empty;
        public PromptStatus Status { get; set; }
        public string? Response { get; set; }
    }
}
