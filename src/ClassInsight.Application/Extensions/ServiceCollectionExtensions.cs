using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using System.Reflection;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Domain.Services;

namespace ClassInsight.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // Registra o MediatR (Handlers)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Registra o FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Registra o Serviço de Domínio (Strategy)
        // Scoped = Criado uma vez por requisição HTTP
        services.AddScoped<IDuaStrategy, DuaPedagogicalStrategy>();

        return services;
    }
}