using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="SaleItem"/> entity.
/// Tests validate various scenarios including valid data and
/// validation failures due to empty ProductId, zero quantity,
/// and negative unit price.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests that a valid SaleItem passes validation successfully.
    /// </summary>
    [Fact(DisplayName = "Valid sale item should pass validation")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldBeValid()
    {
        var item = SaleItemTestData.GenerateValidSaleItem();

        var result = item.Validate();

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that a SaleItem with an empty ProductId fails validation.
    /// </summary>
    [Fact(DisplayName = "Sale item with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldBeInvalid()
    {
        var item = SaleItemTestData.GenerateWithEmptyProductId();

        var result = item.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("ProductId", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that a SaleItem with zero quantity fails validation.
    /// </summary>
    [Fact(DisplayName = "Sale item with zero quantity should fail validation")]
    public void Given_ZeroQuantity_When_Validated_Then_ShouldBeInvalid()
    {
        var item = SaleItemTestData.GenerateWithZeroQuantity();

        var result = item.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("Quantity", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that a SaleItem with a negative unit price fails validation.
    /// </summary>
    [Fact(DisplayName = "Sale item with negative unit price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldBeInvalid()
    {
        var item = SaleItemTestData.GenerateWithNegativeUnitPrice();

        var result = item.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("price", System.StringComparison.OrdinalIgnoreCase));
    }
}
