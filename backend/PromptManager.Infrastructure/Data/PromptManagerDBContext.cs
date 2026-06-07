using Microsoft.EntityFrameworkCore;
using PromptManager.Application.Common.Interfaces;
using PromptManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PromptManager.Infrastructure.Data
{
    public class PromptManagerDBContext : DbContext, IPromptManagerDbContext
    {
        public DbSet<Prompt> Prompts { get; set; }

        public PromptManagerDBContext(DbContextOptions<PromptManagerDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prompt>().Property(p => p.Text).IsRequired().HasMaxLength(2000);
        }
    }
}
