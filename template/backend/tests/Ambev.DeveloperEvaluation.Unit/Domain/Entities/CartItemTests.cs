using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="CartItem"/> entity.
/// Tests validate various scenarios including valid data and
/// validation failures due to empty ProductId, zero quantity,
/// and negative unit price.
/// </summary>
public class CartItemTests
{
    /// <summary>
    /// Tests that a valid CartItem passes validation successfully.
    /// </summary>
    [Fact(DisplayName = "Valid cart item should pass validation")]
    public void Given_ValidCartItem_When_Validated_Then_ShouldBeValid()
    {
        var item = CartItemTestData.GenerateValidCartItem();

        var result = item.Validate();

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that a CartItem with an empty ProductId fails validation.
    /// </summary>
    [Fact(DisplayName = "Cart item with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldBeInvalid()
    {
        var item = CartItemTestData.GenerateWithEmptyProductId();

        var result = item.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("ProductId", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that a CartItem with zero quantity fails validation.
    /// </summary>
    [Fact(DisplayName = "Cart item with zero quantity should fail validation")]
    public void Given_ZeroQuantity_When_Validated_Then_ShouldBeInvalid()
    {
        var item = CartItemTestData.GenerateWithZeroQuantity();

        var result = item.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("Quantity", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that a CartItem with a negative unit price fails validation.
    /// </summary>
    [Fact(DisplayName = "Cart item with negative unit price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldBeInvalid()
    {
        var item = CartItemTestData.GenerateWithNegativeUnitPrice();

        var result = item.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("price", System.StringComparison.OrdinalIgnoreCase));
    }
}
