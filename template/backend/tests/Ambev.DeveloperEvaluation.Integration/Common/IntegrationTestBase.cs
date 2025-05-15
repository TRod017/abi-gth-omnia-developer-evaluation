using System.Net.Http;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

/// <summary>
/// Base class for integration tests.
/// </summary>
public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient Client;

    public IntegrationTestBase(CustomWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
    }
}
