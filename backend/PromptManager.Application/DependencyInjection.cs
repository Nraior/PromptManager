using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PromptManager.Application.Common.Behaviours;
using System.Reflection;

namespace PromptManager.Application
{
    public static class DependencyInjection
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddApplicationServices()
            {
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                });

                return services;
            }
        }
    }
}
