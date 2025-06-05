using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microsoft.Extensions.Hosting;

namespace Ambev.DeveloperEvaluation.Common.Logging
{
    /// <summary> Add default Logging configuration to project. This configuration supports Serilog logs with DataDog compatible output.</summary>
    public static class LoggingExtension
    {
        /// <summary>
        /// The destructuring options builder configured with default destructurers and a custom DbUpdateExceptionDestructurer.
        /// </summary>
        static readonly DestructuringOptionsBuilder _destructuringOptionsBuilder = new DestructuringOptionsBuilder()
            .WithDefaultDestructurers()
            // Corrigido para passar array de destructurers conforme assinatura do método
            .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() });

        /// <summary>
        /// A filter predicate to exclude log events with specific criteria.
        /// </summary>
        static readonly Func<LogEvent, bool> _filterPredicate = exclusionPredicate =>
        {
            if (exclusionPredicate.Level != LogEventLevel.Information) return true;

            exclusionPredicate.Properties.TryGetValue("StatusCode", out var statusCode);
            exclusionPredicate.Properties.TryGetValue("Path", out var path);

            var excludeByStatusCode = statusCode == null || statusCode.ToString().Equals("200");
            var excludeByPath = path?.ToString().Contains("/health") ?? false;

            return excludeByStatusCode && excludeByPath;
        };

        /// <summary>
        /// This method configures the logging with commonly used features for DataDog integration.
        /// </summary>
        /// <param name="builder">The <see cref="WebApplicationBuilder" /> to add services to.</param>
        /// <returns>A <see cref="WebApplicationBuilder"/> that can be used to further configure the API services.</returns>
        /// <remarks>
        /// <para>Logging output are diferents on Debug and Release modes.</para>
        /// </remarks> 
        public static WebApplicationBuilder AddDefaultLogging(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            // Pula configuração de MongoDB se estiver em ambiente de testes
            if (builder.Environment.IsEnvironment("Testing"))
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

                builder.Host.UseSerilog();
                builder.Services.AddLogging();
                return builder;
            }

            // Lê a configuração do MongoDB do appsettings.json
            var mongoSettings = configuration.GetSection("MongoSettings");
            var mongoConnectionString = mongoSettings.GetValue<string>("ConnectionString");
            var mongoDatabaseName = mongoSettings.GetValue<string>("Database");

            // Cria o cliente e o database explicitamente para o Serilog sink
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails(_destructuringOptionsBuilder)
                .Filter.ByExcluding(_filterPredicate)

                // Sink para console
                .WriteTo.Console(
                    outputTemplate: Debugger.IsAttached
                        ? "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
                        : "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}",
                    theme: Debugger.IsAttached ? SystemConsoleTheme.Colored : null
                )

                // Sink para arquivo
                .WriteTo.File(
                    "logs/log-.txt",
                    // Resolve ambiguidade especificando o namespace completo do RollingInterval
                    rollingInterval: Serilog.RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
                )

                // *** INCLUI SINK MONGODB PARA ENVIO DE LOGS AO BANCO ***
                .WriteTo.MongoDB(
                    mongoDatabase,       // Passa o IMongoDatabase explicitamente para garantir uso do banco correto
                    collectionName: "logs" // Nome da coleção no MongoDB onde os logs serão armazenados
                                           // OBS: este parâmetro "databaseName" NÃO existe na versão atual do pacote.
                )

                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddLogging();

            return builder;
        }

        /// <summary>Adds middleware for Swagger documetation generation.</summary>
        /// <param name="app">The <see cref="WebApplication"/> instance this method extends.</param>
        /// <returns>The <see cref="WebApplication"/> for Swagger documentation.</returns>
        public static WebApplication UseDefaultLogging(this WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILogger<Logger>>();

            var mode = Debugger.IsAttached ? "Debug" : "Release";
            logger.LogInformation("Logging enabled for '{Application}' on '{Environment}' - Mode: {Mode}", app.Environment.ApplicationName, app.Environment.EnvironmentName, mode);
            return app;

        }
    }
}
