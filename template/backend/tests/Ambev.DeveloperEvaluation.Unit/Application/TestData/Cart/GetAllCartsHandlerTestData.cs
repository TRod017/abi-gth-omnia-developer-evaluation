using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

/// <summary>
/// Provides methods to generate test data for cart-related tests.
/// </summary>
public static class CartHandlerTestData
{
    private static readonly Faker<CartItem> cartItemFaker = new Faker<CartItem>()
        .RuleFor(i => i.Id, f => f.Random.Guid())
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 500))
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 10))
        .RuleFor(i => i.CreatedAt, f => f.Date.Past())
        .RuleFor(i => i.UpdatedAt, f => f.Date.Recent());

    private static readonly Faker<Ambev.DeveloperEvaluation.Domain.Entities.Cart> cartFaker = new Faker<Ambev.DeveloperEvaluation.Domain.Entities.Cart>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.Status, f => f.PickRandom<CartStatus>())
        .RuleFor(c => c.CreatedAt, f => f.Date.Past())
        .RuleFor(c => c.UpdatedAt, f => f.Date.Recent())
        .RuleFor(c => c.Items, f => cartItemFaker.Generate(f.Random.Number(1, 5)));

    /// <summary>
    /// Generates a list of valid cart entities.
    /// </summary>
    public static List<Ambev.DeveloperEvaluation.Domain.Entities.Cart> GenerateCarts(int count = 5) => cartFaker.Generate(count);
}
