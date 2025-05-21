using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Sale"/> entity.
/// Tests include:
/// - Validation of a valid Sale instance,
/// - Validation failure when required fields are missing,
/// - Enforcement of business rules, such as item quantity limits throwing exceptions.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that a valid Sale passes validation successfully.
    /// </summary>
    [Fact(DisplayName = "Valid sale should pass validation")]
    public void Given_ValidSale_When_Validated_Then_ShouldBeValid()
    {
        var sale = SaleTestData.GenerateValidSale();
        var result = sale.Validate();

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that a Sale with empty CartId fails validation.
    /// </summary>
    [Fact(DisplayName = "Sale with empty CartId should fail validation")]
    public void Given_EmptyCartId_When_Validated_Then_ShouldBeInvalid()
    {
        var sale = SaleTestData.GenerateValidSale();
        sale.CartId = Guid.Empty;

        var result = sale.Validate();

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Detail.Contains("Cart ID", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tests that a Sale with an item quantity exceeding the allowed limit
    /// throws a DomainException due to business rule violation.
    /// </summary>
    [Fact(DisplayName = "Sale with item quantity above limit should fail business rules")]
    public void Given_SaleWithItemQuantityAboveLimit_When_Validated_Then_ShouldThrowDomainException()
    {
        var sale = SaleTestData.GenerateValidSale();
        sale.Items.First().Quantity = 21;

        var ex = Assert.Throws<DomainException>(() => sale.EnsureBusinessRulesAreMet());
        Assert.Contains("exceeds the limit", ex.Message);
    }
}
