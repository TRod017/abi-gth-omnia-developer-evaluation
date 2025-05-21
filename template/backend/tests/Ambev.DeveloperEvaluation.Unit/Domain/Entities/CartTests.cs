using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Cart"/> entity.
/// Tests include:
/// - Validation of a valid Cart instance,
/// - Validation failure when Cart status is unknown,
/// - Enforcement of business rules, such as item quantity limits throwing exceptions.
/// </summary>
public class CartTests
{
    /// <summary>
    /// Tests that a valid Cart passes validation successfully.
    /// </summary>
    [Fact(DisplayName = "Valid cart should pass validation")]
    public void Given_ValidCart_When_Validated_Then_ShouldBeValid()
    {
        var cart = CartTestData.GenerateValidCart();
        var result = cart.Validate();

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that a Cart with an unknown status fails validation.
    /// </summary>
    [Fact(DisplayName = "Cart with unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldBeInvalid()
    {
        var cart = CartTestData.GenerateCartWithInvalidStatus();
        var result = cart.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("status", System.StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that a Cart with an item quantity exceeding the allowed limit
    /// throws a DomainException due to business rule violation.
    /// </summary>
    [Fact(DisplayName = "Cart with item quantity above limit should fail business rules")]
    public void Given_CartWithItemQuantityAboveLimit_When_Validated_Then_ShouldThrowDomainException()
    {
        var cart = CartTestData.GenerateValidCart();

        // Access the first item using LINQ and set quantity above limit
        cart.Items.First().Quantity = 21;

        var ex = Assert.Throws<DomainException>(() => cart.EnsureBusinessRulesAreMet());
        Assert.Contains("exceeds the limit", ex.Message);
    }
}
