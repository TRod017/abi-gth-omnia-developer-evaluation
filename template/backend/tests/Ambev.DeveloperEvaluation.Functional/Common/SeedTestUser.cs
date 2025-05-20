using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.FunctionalTests.Common;

/// <summary>
/// Provides database seeding methods for functional tests using the in-memory database.
/// </summary>
public static class DatabaseSeeder
{
    /// <summary>
    /// Seeds the in-memory test database with a default user for authentication tests.
    /// </summary>
    /// <param name="serviceProvider">The service provider from which to retrieve the DbContext.</param>
    public static void SeedTestUser(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        // Avoids duplicate user creation
        if (!context.Users.Any(u => u.Email == "teste@gmail.com"))
        {
            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = "teste",
                Email = "teste@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                Phone = "21999999999",
                Status = UserStatus.Active,
                Role = UserRole.Customer,
                CreatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
