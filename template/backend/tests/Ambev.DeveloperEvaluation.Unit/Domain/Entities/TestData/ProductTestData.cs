using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides test data generation for Product entities using the Bogus library.
/// Centralizes creation of valid and invalid Product instances for testing purposes.
/// </summary>
public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.AvailableQuantity, f => f.Random.Int(0, 500));

    /// <summary>
    /// Generates a valid Product with realistic data.
    /// </summary>
    /// <returns>A Product with all valid properties.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a Product with an empty name for validation failure testing.
    /// </summary>
    /// <returns>A Product with an empty Name property.</returns>
    public static Product GenerateWithEmptyName()
    {
        var product = GenerateValidProduct();
        product.Name = string.Empty;
        return product;
    }

    /// <summary>
    /// Generates a Product with an empty description for validation failure testing.
    /// </summary>
    /// <returns>A Product with an empty Description property.</returns>
    public static Product GenerateWithEmptyDescription()
    {
        var product = GenerateValidProduct();
        product.Description = string.Empty;
        return product;
    }

    /// <summary>
    /// Generates a Product with a negative unit price for validation failure testing.
    /// </summary>
    /// <returns>A Product with a negative UnitPrice property.</returns>
    public static Product GenerateWithNegativePrice()
    {
        var product = GenerateValidProduct();
        product.UnitPrice = -1;
        return product;
    }

    /// <summary>
    /// Generates a Product with a negative available quantity for validation failure testing.
    /// </summary>
    /// <returns>A Product with a negative AvailableQuantity property.</returns>
    public static Product GenerateWithNegativeQuantity()
    {
        var product = GenerateValidProduct();
        product.AvailableQuantity = -10;
        return product;
    }
}
