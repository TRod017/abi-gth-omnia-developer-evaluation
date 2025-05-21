using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides test data generation for Sale and SaleItem entities using the Bogus library.
/// This class centralizes creation of valid and invalid Sale instances for testing purposes.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.SaleId, f => f.Random.Guid())                    // Adicionado SaleId
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.ProductName, f => f.Commerce.ProductName())    // Adicionado ProductName
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 500));

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.CartId, f => f.Random.Guid())
        .RuleFor(c => c.SaleNumber, f => $"VEN-{f.Random.Number(1000, 9999)}")
        .RuleFor(c => c.Branch, f => f.Company.CompanyName())
        .RuleFor(c => c.IsCancelled, f => f.PickRandom(new[] { false , true }))
        .RuleFor(c => c.CreatedAt, f => f.Date.Past())
        .RuleFor(c => c.Items, f => new System.Collections.Generic.List<SaleItem>());

    /// <summary>
    /// Generates a valid Sale instance with one valid SaleItem.
    /// </summary>
    /// <returns>A Sale with valid properties and one item.</returns>
    public static Sale GenerateValidSale()
    {
        var sale = SaleFaker.Generate();

        // Adds a valid sale item
        sale.Items.Add(SaleItemFaker.Generate());

        return sale;
    }

    /// <summary>
    /// Generates a Sale instance with an invalid status (Unknown).
    /// </summary>
    /// <returns>A Sale with a valid user and items but invalid status.</returns>
    public static Sale GenerateSaleWithInvalidStatus()
    {
        var sale = GenerateValidSale();
        sale.CartId = System.Guid.Empty;
        return sale;
    }
}
