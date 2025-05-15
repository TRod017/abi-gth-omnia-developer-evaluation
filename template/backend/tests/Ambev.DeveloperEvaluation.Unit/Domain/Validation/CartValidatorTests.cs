using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for the CartValidator class.
/// </summary>
public class CartValidatorTests
{
    private readonly CartValidator _validator = new();

    [Fact(DisplayName = "Cart with valid data should pass validation")]
    public void Given_ValidCart_When_Validated_Then_ShouldNotHaveErrors()
    {
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Open
        };

        var result = _validator.TestValidate(cart);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Cart with empty UserId should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var cart = new Cart
        {
            UserId = Guid.Empty,
            Status = CartStatus.Open
        };

        var result = _validator.TestValidate(cart);
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    [Fact(DisplayName = "Cart with Unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError()
    {
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Unknown
        };

        var result = _validator.TestValidate(cart);
        result.ShouldHaveValidationErrorFor(c => c.Status);
    }
}
