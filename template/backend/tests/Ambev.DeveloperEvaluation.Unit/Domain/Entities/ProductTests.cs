using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover the validation logic applied to product data.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Tests that the product is valid when all fields are correctly filled.
    /// </summary>
    [Fact(DisplayName = "Product should be valid when all fields are correctly filled")]
    public void Given_ValidProduct_When_Validated_Then_ShouldBeValid()
    {
        var product = ProductTestData.GenerateValidProduct();
        var result = product.Validate();

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that the product validation fails when the name is empty.
    /// </summary>
    [Fact(DisplayName = "Product with empty name should fail validation")]
    public void Given_EmptyName_When_Validated_Then_ShouldReturnInvalid()
    {
        var product = ProductTestData.GenerateWithEmptyName();
        var result = product.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("name", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that the product validation fails when the description is empty.
    /// </summary>
    [Fact(DisplayName = "Product with empty description should fail validation")]
    public void Given_EmptyDescription_When_Validated_Then_ShouldReturnInvalid()
    {
        var product = ProductTestData.GenerateWithEmptyDescription();
        var result = product.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("description", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that the product validation fails when the price is negative.
    /// </summary>
    [Fact(DisplayName = "Product with negative price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldReturnInvalid()
    {
        var product = ProductTestData.GenerateWithNegativePrice();
        var result = product.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("price", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that the product validation fails when the quantity is negative.
    /// </summary>
    [Fact(DisplayName = "Product with negative quantity should fail validation")]
    public void Given_NegativeQuantity_When_Validated_Then_ShouldReturnInvalid()
    {
        var product = ProductTestData.GenerateWithNegativeQuantity();
        var result = product.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("quantity", System.StringComparison.OrdinalIgnoreCase));
    }
}
