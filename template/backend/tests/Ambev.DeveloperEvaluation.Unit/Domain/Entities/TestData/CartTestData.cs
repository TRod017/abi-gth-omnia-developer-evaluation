using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides test data generation for Cart and CartItem entities using the Bogus library.
/// This class centralizes creation of valid and invalid Cart instances for testing purposes.
/// </summary>
public static class CartTestData
{
    private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
        .RuleFor(i => i.CartId, f => f.Random.Guid())                    // Adicionado CartId
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())    // Adicionado ProductName
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 500));

    private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
        .RuleFor(c => c.UserId, f => f.Random.Guid())
         // Excluindo CartStatus.Unknown
        .RuleFor(c => c.Status, f => f.PickRandom(new[] { CartStatus.Open, CartStatus.Confirmed }))
        .RuleFor(c => c.CreatedAt, f => f.Date.Past())
        .RuleFor(c => c.Items, f => new System.Collections.Generic.List<CartItem>());

    /// <summary>
    /// Generates a valid Cart instance with one valid CartItem.
    /// </summary>
    /// <returns>A Cart with valid properties and one item.</returns>
    public static Cart GenerateValidCart()
    {
        var cart = CartFaker.Generate();

        // Adds a valid cart item
        cart.Items.Add(CartItemFaker.Generate());

        return cart;
    }

    /// <summary>
    /// Generates a Cart instance with an invalid status (Unknown).
    /// </summary>
    /// <returns>A Cart with a valid user and items but invalid status.</returns>
    public static Cart GenerateCartWithInvalidStatus()
    {
        var cart = GenerateValidCart();
        cart.Status = CartStatus.Unknown;
        return cart;
    }
}
