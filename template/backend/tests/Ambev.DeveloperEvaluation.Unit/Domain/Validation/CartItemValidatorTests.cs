using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for the CartItemValidator class.
/// </summary>
public class CartItemValidatorTests
{
    private readonly CartItemValidator _validator = new();

    [Fact(DisplayName = "CartItem with valid data should pass validation")]
    public void Given_ValidCartItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        var item = new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = 99.99m
        };

        var result = _validator.TestValidate(item);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "CartItem with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
    {
        var item = new CartItem
        {
            ProductId = Guid.Empty,
            Quantity = 2,
            UnitPrice = 99.99m
        };

        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(i => i.ProductId);
    }

    [Fact(DisplayName = "CartItem with zero quantity should fail validation")]
    public void Given_ZeroQuantity_When_Validated_Then_ShouldHaveError()
    {
        var item = new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 0,
            UnitPrice = 99.99m
        };

        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(i => i.Quantity);
    }

    [Fact(DisplayName = "CartItem with negative price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldHaveError()
    {
        var item = new CartItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2,
            UnitPrice = -10m
        };

        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(i => i.UnitPrice);
    }
}
