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
            EnsureCanTransitionTo(PromptStatus.Processing);
            Status = PromptStatus.Processing;
        }

        public void SetSuccessful(string processedResponse)
        {
            EnsureCanTransitionTo(PromptStatus.Successful);
            Response = processedResponse;
            Status = PromptStatus.Successful;
        }

        public void SetError(string? error)
        {
            EnsureCanTransitionTo(PromptStatus.Failed);
            Status = PromptStatus.Failed;
            Response = error;
        }

        private void EnsureCanTransitionTo(PromptStatus newStatus)
        {
            if (Status == newStatus)
            {
                return;
            }

            var isAllowed = Status switch
            {
                PromptStatus.Received => newStatus == PromptStatus.Processing || newStatus == PromptStatus.Failed,
                PromptStatus.Processing => newStatus == PromptStatus.Successful || newStatus == PromptStatus.Failed,
                PromptStatus.Failed => newStatus == PromptStatus.Processing,
                PromptStatus.Successful => false,
                _ => false
            };

            if (!isAllowed)
            {
                throw new InvalidOperationException($"Prompt status cannot change from {Status} to {newStatus}.");
            }
        }
    }
}
