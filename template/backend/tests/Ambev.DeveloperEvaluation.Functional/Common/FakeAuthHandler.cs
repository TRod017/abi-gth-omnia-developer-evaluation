using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ambev.DeveloperEvaluation.FunctionalTests.Common.Auth;

/// <summary>
/// Fake authentication handler used for functional tests.
/// </summary>
/// <remarks>
/// This handler automatically authenticates requests using a predefined identity and claims.
/// It enables testing of protected endpoints without relying on real authentication mechanisms.
/// </remarks>
public class FakeAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    /// <summary>
    /// Handles the authentication by returning a valid <see cref="AuthenticateResult"/> 
    /// with a fake user identity and claims.
    /// </summary>
    /// <returns>A successful authentication result with a predefined identity.</returns>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Email, "test@example.com"),
            new Claim(ClaimTypes.Role, "Admin") // or "Customer"
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
