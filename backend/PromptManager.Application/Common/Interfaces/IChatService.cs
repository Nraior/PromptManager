using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Application.Common.Interfaces
{
    public interface IChatService
    {
        Task<string> GetResponseAsync(string prompt, CancellationToken ct);
    }
}
