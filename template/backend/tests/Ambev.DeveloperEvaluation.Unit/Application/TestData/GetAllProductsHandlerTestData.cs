using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods to generate test data for GetAllProductsHandler tests.
/// </summary>
public static class GetAllProductsHandlerTestData
{
    private static readonly Faker<Product> faker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 10000))
        .RuleFor(p => p.AvailableQuantity, f => f.Random.Number(1, 100));

    public static List<Product> GenerateProducts(int count = 5) => faker.Generate(count);
}
