using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Product;

/// <summary>
/// Provides methods to generate test data for UpdateProductHandler tests.
/// </summary>
public static class UpdateCartHandlerTestData
{
    private static readonly Faker<UpdateProductCommand> faker = new Faker<UpdateProductCommand>()
        .RuleFor(p => p.Id, f => Guid.NewGuid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 10000))
        .RuleFor(p => p.AvailableQuantity, f => f.Random.Number(1, 100));

    public static UpdateProductCommand GenerateValidCommand() => faker.Generate();
}
