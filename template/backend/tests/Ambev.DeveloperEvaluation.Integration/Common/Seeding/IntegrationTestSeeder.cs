using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Integration.Common.Seeding;

/// <summary>
/// Provides helper methods to seed test data into the in-memory database for integration tests.
/// </summary>
public static class IntegrationTestSeeder
{
    /// <summary>
    /// Seeds a user with predefined credentials for authentication tests.
    /// </summary>
    /// <param name="scopeFactory">Scope factory to resolve database context.</param>
    public static async Task SeedTestUserAsync(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        if (!context.Users.Any(u => u.Email == "testuser@example.com"))
        {
            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                Phone = "+5521999999999",
                Role = UserRole.Customer,
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
        }
    }
}
