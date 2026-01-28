using System.Reflection;
using System.Text;
using ClassInsight.Application.Extensions;
using ClassInsight.Infrastructure.Extensions;
using ClassInsight.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. CONFIGURAÇÃO DAS CAMADAS (CLEAN ARCHITECTURE)
// =========================================================================
// TRUE = Modo Gratuito (Fakes). Mude para FALSE se usar chaves reais.
bool usarModoGratuito = true; 

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration, usarModoGratuito);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// =========================================================================
// 2. CONFIGURAÇÃO DO SWAGGER (DOCUMENTAÇÃO E SEGURANÇA)
// =========================================================================
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "ClassInsight AI - Plataforma de Inclusão Pedagógica", 
        Version = "v1",
        Description = "API de análise pedagógica baseada em DUA e IA. Projetada para garantir anonimização e segurança (LGPD).",
        Contact = new OpenApiContact { Name = "Comunidade ClassInsight", Email = "suporte@classinsight.com" }
    });

    // Configuração do Botão 'Authorize' (Cadeado)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } 
            },
            Array.Empty<string>()
        }
    });

    // Habilita a leitura dos comentários XML para exibir descrições no Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// =========================================================================
// 3. CONFIGURAÇÃO DE AUTENTICAÇÃO E AUTORIZAÇÃO (JWT)
// =========================================================================
var jwtKey = builder.Configuration["Jwt:Key"] ?? "Chave_Super_Secreta_Para_O_Tutorial_ClassInsight_Community_2026";
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// =========================================================================
// 4. PIPELINE DE EXECUÇÃO (MIDDLEWARES)
// =========================================================================
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>(); // Captura erros antes de tudo

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClassInsight v1");
        c.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }