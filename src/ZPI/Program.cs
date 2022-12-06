using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Serilog;
using ZPI.API;
using ZPI.API.Configuration;
using ZPI.AspNetCore.Utils;
using ZPI.Core;
using ZPI.Integrations;
using ZPI.Persistance;

#if DEBUG
var startupConfig = new ConfigurationBuilder().Apply(b => StartupConfigurationHelper.LoadStartupConfiguration(b, true)).Build();
#else
var startupConfig = new ConfigurationBuilder().Apply(b => StartupConfigurationHelper.LoadStartupConfiguration(b, false)).Build();
#endif

const string CorsPolicyName = "CORS";

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(startupConfig, Constants.ConfigSections.Serilog)
    .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
    .CreateLogger();

var jsonSerializerConfigurator = JsonSerializerSettingsConfigurator.Build((settings) =>
{
    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    settings.Converters.Add(new StringEnumConverter());

    settings.NullValueHandling = NullValueHandling.Ignore;
    settings.TypeNameHandling = TypeNameHandling.None;

    // Configure NodaTime
    settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
});

WebApplication BuildWebApplication()
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureAppConfiguration((ctx, builder) => builder.AddConfiguration(startupConfig));

    builder.Logging.ClearProviders();
    builder.Host.UseSerilog(Log.Logger, true);

    builder.Services.AddAuthorization(o =>
  {
      o.DefaultPolicy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
          .Build();
  })
      .AddAuthentication(options => builder.Configuration.Bind(Constants.ConfigSections.Authentication.DefaultConfig, options))
      .AddJwtBearer(options => builder.Configuration.Bind(Constants.ConfigSections.Authentication.JwtBearer, options));

    builder.Services.AddJsonSerializerConfigurator(jsonSerializerConfigurator, true);
    builder.Services.AddCors(options =>
    {
        var corsOptions = builder.Configuration.GetSection<CorsPolicy>(Constants.ConfigSections.CorsPolicy);
        if (corsOptions == null)
        {
            Log.Warning("No cors policy was found in the configuration.");
        }
        else
        {
            options.AddDefaultPolicy(corsOptions);
        }
    });

    // Add services to the container.
    builder.Services.AddControllers()
        .AddNewtonsoftJson(options => jsonSerializerConfigurator.ApplyTo(options.SerializerSettings));

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddMemoryCache();
    //Register application components
    builder.Services.AddCoreModule();
    builder.Services.AddAPIModule();
    builder.Services.AddPersistanceModule(builder.Configuration);
    builder.Services.AddIntegrationsModule(builder.Configuration);

    var app = builder.Build();

    // Initialize application components
    app.UseAPIModule();
    app.UsePersistanceModule();



    // app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseCors();
    app.MapControllers().RequireAuthorization();

    return app;
}

try
{
    Log.Information("Application initializing...");

    var app = BuildWebApplication();

    Log.Information("Application starting...");

    await app.RunAsync();

    Log.Information("Application terminated safely.");
    Environment.ExitCode = 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
    Environment.ExitCode = 1;
}
finally
{
    Log.CloseAndFlush();
}
