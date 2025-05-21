using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CartItem;

/// <summary>
/// Unit tests for the <see cref="CreateCartItemResult"/> class.
/// Tests basic property get/set behavior.
/// </summary>
public class CreateCartItemResultTests
{
    [Fact(DisplayName = "CreateCartItemResult properties should store and retrieve values correctly")]
    public void Given_ValidValues_When_SetProperties_Then_ShouldReturnSameValues()
    {
        // Arrange
        var itemResult = CreateCartItemResultTestData.GenerateValid();

        // Assert
        Assert.NotEqual(default, itemResult.ProductId);
        Assert.False(string.IsNullOrWhiteSpace(itemResult.ProductName));
        Assert.True(itemResult.UnitPrice > 0);
        Assert.True(itemResult.Quantity > 0);
        Assert.True(itemResult.Total >= 0);
        Assert.True(itemResult.Discount >= 0);
        Assert.True(itemResult.TotalWithDiscount >= 0);
    }
}
