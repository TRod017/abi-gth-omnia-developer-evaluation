using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CartItemTests
{
    [Fact(DisplayName = "Valid cart item should pass validation")]
    public void Given_ValidCartItem_When_Validated_Then_ShouldBeValid()
    {
        var item = new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = 100
        };

        var result = item.Validate();
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact(DisplayName = "Cart item with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldBeInvalid()
    {
        var item = new CartItem
        {
            ProductId = Guid.Empty,
            Quantity = 1,
            UnitPrice = 10
        };

        var result = item.Validate();
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("Product ID", StringComparison.OrdinalIgnoreCase));
    }

    [Fact(DisplayName = "Cart item with zero quantity should fail validation")]
    public void Given_ZeroQuantity_When_Validated_Then_ShouldBeInvalid()
    {
        var item = new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 0,
            UnitPrice = 10
        };

        var result = item.Validate();
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("Quantity", StringComparison.OrdinalIgnoreCase));
    }

    [Fact(DisplayName = "Cart item with negative unit price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldBeInvalid()
    {
        var item = new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = -10
        };

        var result = item.Validate();
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("price", StringComparison.OrdinalIgnoreCase));
    }
}
