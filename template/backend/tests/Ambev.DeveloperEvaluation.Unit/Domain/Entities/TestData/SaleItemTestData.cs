using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides test data generation for SaleItem using the Bogus library.
/// </summary>
public static class SaleItemTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.SaleId, f => f.Random.Guid()) // Adiciona SaleId para validação
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName()) // Adiciona ProductName para validação
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 500));

    /// <summary>
    /// Generates a valid SaleItem with populated fields.
    /// </summary>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a SaleItem with an empty ProductId for negative test.
    /// </summary>
    public static SaleItem GenerateWithEmptyProductId()
    {
        var item = GenerateValidSaleItem();
        item.ProductId = Guid.Empty;
        return item;
    }

    /// <summary>
    /// Generates a SaleItem with zero quantity for negative test.
    /// </summary>
    public static SaleItem GenerateWithZeroQuantity()
    {
        var item = GenerateValidSaleItem();
        item.Quantity = 0;
        return item;
    }

    /// <summary>
    /// Generates a SaleItem with negative unit price for negative test.
    /// </summary>
    public static SaleItem GenerateWithNegativeUnitPrice()
    {
        var item = GenerateValidSaleItem();
        item.UnitPrice = -5;
        return item;
    }
}
