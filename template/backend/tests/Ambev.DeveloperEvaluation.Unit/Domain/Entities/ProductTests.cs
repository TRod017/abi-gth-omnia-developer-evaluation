using Ambev.DeveloperEvaluation.Domain.Entities;
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
    /// Tests that validation passes when all product fields are valid.
    /// </summary>
    [Fact(DisplayName = "Product should be valid when all fields are correctly filled")]
    public void Given_ValidProduct_When_Validated_Then_ShouldBeValid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when product name is empty.
    /// </summary>
    [Fact(DisplayName = "Product with empty name should fail validation")]
    public void Given_EmptyName_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateWithEmptyName();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("name", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that validation fails when product description is empty.
    /// </summary>
    [Fact(DisplayName = "Product with empty description should fail validation")]
    public void Given_EmptyDescription_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateWithEmptyDescription();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("description", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that validation fails when product unit price is negative.
    /// </summary>
    [Fact(DisplayName = "Product with negative price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateWithNegativePrice();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("price", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that validation fails when available quantity is negative.
    /// </summary>
    [Fact(DisplayName = "Product with negative quantity should fail validation")]
    public void Given_NegativeQuantity_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateWithNegativeQuantity();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("quantity", StringComparison.OrdinalIgnoreCase));
    }
}
