using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

/// <summary>
/// Provides test data generation for <see cref="CreateCartItemResult"/> using Bogus.
/// </summary>
public static class CreateCartItemResultTestData
{
    private static readonly Faker<CreateCartItemResult> faker = new Faker<CreateCartItemResult>()
        .RuleFor(c => c.ProductId, f => Guid.NewGuid())
        .RuleFor(c => c.ProductName, f => f.Commerce.ProductName())
        .RuleFor(c => c.UnitPrice, f => f.Finance.Amount(1, 1000))
        .RuleFor(c => c.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(c => c.Total, (f, c) => c.UnitPrice * c.Quantity)
        .RuleFor(c => c.Discount, f => f.Finance.Amount(0, 50))
        .RuleFor(c => c.TotalWithDiscount, (f, c) => (c.Total - c.Discount) > 0 ? c.Total - c.Discount : 0);

    /// <summary>
    /// Generates a valid <see cref="CreateCartItemResult"/> instance with realistic data.
    /// </summary>
    public static CreateCartItemResult GenerateValid() => faker.Generate();
}
