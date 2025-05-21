using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Functional.Common.Auth;
using Ambev.DeveloperEvaluation.Functional.Common.Seeding;

namespace Ambev.DeveloperEvaluation.Functional.Common;

/// <summary>
/// Custom factory for creating a test server with the application pipeline configured for functional tests.
/// </summary>
/// <typeparam name="TProgram">The entry point class of the application (typically <c>Program</c>).</typeparam>
/// <remarks>
/// This factory sets the environment to "Testing" and can be extended to configure in-memory databases,
/// mock services, authentication, or other test-specific settings.
/// </remarks>
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // ⚠️ Remove the production context configuration if present
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var contextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DefaultContext));
            if (contextDescriptor != null)
            {
                services.Remove(contextDescriptor);
            }

            // ✅ Add in-memory database for testing
            services.AddDbContext<DefaultContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // ✅ Seed the in-memory database with default test data
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                scope.ServiceProvider.SeedTestUser();
            }

            // ✅ Add fake authentication for testing secured endpoints
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("Test", options => { });

            services.PostConfigureAll<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            });
        });
    }
}
