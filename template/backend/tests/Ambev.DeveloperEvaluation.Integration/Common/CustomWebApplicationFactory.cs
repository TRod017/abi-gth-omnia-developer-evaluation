using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Integration.Common.Auth;

namespace Ambev.DeveloperEvaluation.Integration.Common;

/// <summary>
/// Custom factory for creating a test server with in-memory database and fake authentication.
/// </summary>
/// <typeparam name="TProgram">The entry point class of the application (typically <c>Program</c>).</typeparam>
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove o contexto real de produção, se existir
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Registra o banco InMemory
            services.AddDbContext<DefaultContext>(options =>
            {
                options.UseInMemoryDatabase("IntegrationTestDb");
            });

            // Autenticação fake
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("Test", _ => { });

            services.PostConfigureAll<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            });
        });
    }
}
