using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides test data generation for CartItem using the Bogus library.
/// </summary>
public static class CartItemTestData
{
    private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 500));

    /// <summary>
    /// Generates a valid CartItem with populated fields.
    /// </summary>
    public static CartItem GenerateValidCartItem()
    {
        return CartItemFaker.Generate();
    }

    /// <summary>
    /// Generates a CartItem with an empty ProductId to test validation failure.
    /// </summary>
    public static CartItem GenerateWithEmptyProductId()
    {
        var item = GenerateValidCartItem();
        item.ProductId = Guid.Empty;
        return item;
    }

    /// <summary>
    /// Generates a CartItem with zero quantity to test validation failure.
    /// </summary>
    public static CartItem GenerateWithZeroQuantity()
    {
        var item = GenerateValidCartItem();
        item.Quantity = 0;
        return item;
    }

    /// <summary>
    /// Generates a CartItem with negative unit price to test validation failure.
    /// </summary>
    public static CartItem GenerateWithNegativeUnitPrice()
    {
        var item = GenerateValidCartItem();
        item.UnitPrice = -5;
        return item;
    }
}
