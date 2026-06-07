using Microsoft.EntityFrameworkCore;
using PromptManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Application.Common.Interfaces
{
    public interface IPromptManagerDbContext
    {
        DbSet<Prompt> Prompts { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
