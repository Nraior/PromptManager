using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
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
                services.AddOllamaResilience(config);

                services.AddScoped<IChatClient>(serviceProvider => {
                    var settings = serviceProvider.GetRequiredService<IOptions<AiSettings>>().Value;
                    return ChatClientFactory.Create(settings);
                });
                services.AddScoped<IChatService, OllamaChatService>();

                return services;
            }

            private IServiceCollection AddOllamaResilience(IConfiguration config)
            {
                var ollamaConfig = config.GetSection("AI:Ollama");
                var timeoutSeconds = ReadPositiveInt(ollamaConfig["TimeoutSeconds"], 60);
                var retryAttempts = ReadNonNegativeInt(ollamaConfig["MaxRetryAttempts"], 2);
                var retryDelayMilliseconds = ReadNonNegativeInt(ollamaConfig["RetryDelayMilliseconds"], 500);
                var circuitBreakerFailureThreshold = ReadPositiveInt(ollamaConfig["CircuitBreakerFailureThreshold"], 3);
                var circuitBreakerBreakSeconds = ReadPositiveInt(ollamaConfig["CircuitBreakerBreakSeconds"], 30);

                services.AddResiliencePipeline<string>(OllamaChatService.ResiliencePipelineName, builder =>
                {
                    builder
                        .AddRetry(new RetryStrategyOptions
                        {
                            MaxRetryAttempts = retryAttempts,
                            Delay = TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                            BackoffType = DelayBackoffType.Exponential
                        })
                        .AddTimeout(new TimeoutStrategyOptions
                        {
                            Timeout = TimeSpan.FromSeconds(timeoutSeconds)
                        })
                        .AddCircuitBreaker(new CircuitBreakerStrategyOptions
                        {
                            FailureRatio = 1.0,
                            MinimumThroughput = circuitBreakerFailureThreshold,
                            SamplingDuration = TimeSpan.FromSeconds(circuitBreakerBreakSeconds),
                            BreakDuration = TimeSpan.FromSeconds(circuitBreakerBreakSeconds)
                        });
                });

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

        private static int ReadPositiveInt(string? value, int fallback)
        {
            return int.TryParse(value, out var parsed) && parsed > 0 ? parsed : fallback;
        }

        private static int ReadNonNegativeInt(string? value, int fallback)
        {
            return int.TryParse(value, out var parsed) && parsed >= 0 ? parsed : fallback;
        }
    }
}
