using Bogus;


namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Product;

/// <summary>
/// Provides methods to generate test data for GetAllProductsHandler tests.
/// </summary>
public static class GetAllCartsHandlerTestData
{
    private static readonly Faker<Ambev.DeveloperEvaluation.Domain.Entities.Product> faker = new Faker<Ambev.DeveloperEvaluation.Domain.Entities.Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 10000))
        .RuleFor(p => p.AvailableQuantity, f => f.Random.Number(1, 100));

    public static List<Ambev.DeveloperEvaluation.Domain.Entities.Product> GenerateProducts(int count = 5) => faker.Generate(count);
}
