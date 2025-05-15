using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods to generate test data for CreateProductHandler tests.
/// </summary>
public static class CreateProductHandlerTestData
{
    private static readonly Faker<CreateProductCommand> faker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 10000))
        .RuleFor(p => p.AvailableQuantity, f => f.Random.Number(1, 100));

    public static CreateProductCommand GenerateValidCommand() => faker.Generate();
}
