using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NodaTime;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ZPI.API.Configuration;

public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.ConfigureForNodaTime();
        options.SchemaGeneratorOptions.CustomTypeMappings[typeof(LocalDate)] = () => new OpenApiSchema
        {
            Type = "string",
            Format = "date",
        };

        options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
        options.UseOneOfForPolymorphism();
        options.UseAllOfForInheritance();

        options.AddSecurityDefinition("BearerJWT", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme.",
        });


        options.SwaggerDoc("all", new OpenApiInfo { Title = "ZPI API", Version = "all" });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerJWT" },
                    },
                    Array.Empty<string>()
                },
            });
    }
}