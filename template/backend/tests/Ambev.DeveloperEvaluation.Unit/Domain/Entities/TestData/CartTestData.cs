using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CartTestData
{
    private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.Status, f => f.PickRandom<CartStatus>())
        .RuleFor(c => c.CreatedAt, f => f.Date.Past());

    public static Cart GenerateValidCart()
    {
        var cart = CartFaker.Generate();
        cart.Items.Add(new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = 100
        });
        return cart;
    }

    public static Cart GenerateCartWithInvalidStatus()
    {
        var cart = GenerateValidCart();
        cart.Status = CartStatus.Unknown;
        return cart;
    }
}
