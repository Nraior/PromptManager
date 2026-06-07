using PromptManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Domain.Entities
{
    public class Prompt
    {
        public Guid Id { get; private set; }
        public DateTime DateAsked { get; private set; } 
        public string Text { get; private set; }
        public PromptStatus Status { get; private set; }
        public string? Response { get; private set; }

        public Prompt(string requestedPrompt)
        {
            Id = Guid.CreateVersion7();
            Text = requestedPrompt;
            DateAsked = DateTime.UtcNow;
            Status = PromptStatus.Received;

        }

        private Prompt()
        {
            Text = null!;
        }

        public void SetProcessing()
        {
            Status = PromptStatus.Processing;
        }

        public void SetSuccessful(string processedResponse)
        {
            Response = processedResponse;
            Status = PromptStatus.Successful;
        }

        public void SetError(string? error)
        {
            Status = PromptStatus.Failed;
            Response = error;
        }
    }
}
