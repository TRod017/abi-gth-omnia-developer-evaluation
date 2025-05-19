using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for the <see cref="CartValidator"/> class.
/// 
/// These tests verify that the Cart entity respects validation rules such as:
/// - UserId must not be empty,
/// - Status must be a valid enum value and not Unknown,
/// - Valid cart data passes validation.
/// </summary>
public class CartValidatorTests
{
    private readonly CartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartValidatorTests"/> class.
    /// </summary>
    public CartValidatorTests()
    {
        _validator = new CartValidator();
    }

    /// <summary>
    /// Tests that a cart with valid data passes validation without errors.
    /// </summary>
    [Fact(DisplayName = "Cart with valid data should pass validation")]
    public void Given_ValidCart_When_Validated_Then_ShouldNotHaveErrors()
    {
        var cart = CartTestData.GenerateValidCart();

        var result = _validator.TestValidate(cart);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the UserId is empty.
    /// </summary>
    [Fact(DisplayName = "Cart with empty UserId should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var cart = CartTestData.GenerateValidCart();
        cart.UserId = System.Guid.Empty;

        var result = _validator.TestValidate(cart);

        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    /// <summary>
    /// Tests that validation fails when the Status is Unknown.
    /// </summary>
    [Fact(DisplayName = "Cart with Unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError()
    {
        var cart = CartTestData.GenerateCartWithInvalidStatus();

        var result = _validator.TestValidate(cart);

        result.ShouldHaveValidationErrorFor(c => c.Status);
    }
}
