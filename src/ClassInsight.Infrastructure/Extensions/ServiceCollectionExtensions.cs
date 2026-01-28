using ClassInsight.Application.Interfaces;
using ClassInsight.Domain.Interfaces;
using ClassInsight.Infrastructure.Configuration;
using ClassInsight.Infrastructure.Data;
using ClassInsight.Infrastructure.Repositories;
using ClassInsight.Infrastructure.Services;
using ClassInsight.Infrastructure.Services.Fakes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppInterfaces = ClassInsight.Application.Interfaces;

namespace ClassInsight.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, bool usarModoGratuito = true)
    {
        // 1. Configurações
        services.Configure<AzureAiSettings>(configuration.GetSection("AzureAi"));
        
        // 2. Banco de Dados (In-Memory)
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ClassInsightDb"));
        services.AddScoped<IRegistroRepository, RegistroRepository>();

        // 3. IA (Real vs Fake)
        if (usarModoGratuito)
        {
            services.AddScoped<IAiService, FakeAiService>();
            services.AddScoped<IGenAiService, FakeGenAiService>();
        }
        else
        {
            services.AddScoped<IAiService, AzureAiService>();
            services.AddScoped<IGenAiService, FakeGenAiService>(); 
        }

        // 4. Observabilidade

        services.AddScoped<AppInterfaces.IMetricsService, ConsoleMetricsService>();

        return services;
    }
}