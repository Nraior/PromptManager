using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PromptManager.Application.Common.Interfaces;
using PromptManager.Application.Common.Options;
using PromptManager.Infrastructure.Data;
using PromptManager.Infrastructure.Factories;
using PromptManager.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromptManager.Infrastructure
{
    public static class DependencyInjection
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddInfrastructureMethods(IConfiguration config)
            {
                services.AddDatabase(config);
                services.AddAIServices(config);
                services.AddBackgroundJobs(config);

                return services;
            }

            private IServiceCollection AddDatabase(IConfiguration config)
            {
                services.AddDbContext<PromptManagerDBContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("PostgresConnection"))
                );

                services.AddScoped<IPromptManagerDbContext>(provider => provider.GetRequiredService<PromptManagerDBContext>());

                return services;
            }


            private IServiceCollection AddAIServices(IConfiguration config)
            {
                services.Configure<AiSettings>(config.GetSection("AI"));
                services.AddScoped<IChatClient>(serviceProvider => {
                    var settings = serviceProvider.GetRequiredService<IOptions<AiSettings>>().Value;
                    return ChatClientFactory.Create(settings);
                });
                services.AddScoped<IChatService, OllamaChatService>();

                return services;
            }

            private IServiceCollection AddBackgroundJobs(IConfiguration config)
            {
                services.AddHangfire(hangfire => hangfire
                    .UsePostgreSqlStorage(c =>
                        c.UseNpgsqlConnection(config.GetConnectionString("PostgresConnection")))
                );

                services.AddHangfireServer();

                return services;
            }


        }

    }
}
