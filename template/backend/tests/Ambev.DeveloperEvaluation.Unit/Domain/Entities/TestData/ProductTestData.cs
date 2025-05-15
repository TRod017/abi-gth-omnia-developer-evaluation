using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating Product test data using the Bogus library.
/// Ensures consistency and realism across test cases.
/// </summary>
public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.AvailableQuantity, f => f.Random.Int(0, 500));

    /// <summary>
    /// Generates a valid Product entity with all fields correctly populated.
    /// </summary>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a Product with empty name, used to test validation failures.
    /// </summary>
    public static Product GenerateWithEmptyName()
    {
        var product = GenerateValidProduct();
        product.Name = string.Empty;
        return product;
    }

    /// <summary>
    /// Generates a Product with empty description, used to test validation failures.
    /// </summary>
    public static Product GenerateWithEmptyDescription()
    {
        var product = GenerateValidProduct();
        product.Description = string.Empty;
        return product;
    }

    /// <summary>
    /// Generates a Product with negative price, used to test validation failures.
    /// </summary>
    public static Product GenerateWithNegativePrice()
    {
        var product = GenerateValidProduct();
        product.UnitPrice = -1;
        return product;
    }

    /// <summary>
    /// Generates a Product with negative quantity, used to test validation failures.
    /// </summary>
    public static Product GenerateWithNegativeQuantity()
    {
        var product = GenerateValidProduct();
        product.AvailableQuantity = -10;
        return product;
    }
}
