using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CartTests
{
    [Fact(DisplayName = "Valid cart should pass validation")]
    public void Given_ValidCart_When_Validated_Then_ShouldBeValid()
    {
        var cart = CartTestData.GenerateValidCart();
        var result = cart.Validate();
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact(DisplayName = "Cart with unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldBeInvalid()
    {
        var cart = CartTestData.GenerateCartWithInvalidStatus();
        var result = cart.Validate();
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("status", StringComparison.OrdinalIgnoreCase));
    }
}
