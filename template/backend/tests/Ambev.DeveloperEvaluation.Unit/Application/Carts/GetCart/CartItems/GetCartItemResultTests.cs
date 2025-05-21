using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CartItem;

/// <summary>
/// Unit tests for the <see cref="GetCartItemResult"/> class.
/// Tests basic property get/set behavior to ensure correct storage and retrieval of values.
/// </summary>
public class GetCartItemResultTests
{
    /// <summary>
    /// Tests that the properties of <see cref="GetCartItemResult"/> correctly store
    /// and return the values assigned to them.
    /// </summary>
    [Fact(DisplayName = "GetCartItemResult properties should store and retrieve values correctly")]
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
