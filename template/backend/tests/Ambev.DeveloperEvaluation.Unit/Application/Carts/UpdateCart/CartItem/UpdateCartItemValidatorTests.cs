using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.UpdateCart.CartItems;

/// <summary>
/// Unit tests for the <see cref="UpdateCartItemValidator"/> class.
/// These tests validate the rules defined for the UpdateCartItemCommand,
/// ensuring that required fields like ProductId, Quantity, and UnitPrice
/// follow the expected constraints.
/// </summary>
public class UpdateCartItemValidatorTests
{
    private readonly UpdateCartItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemValidatorTests"/> class.
    /// </summary>
    public UpdateCartItemValidatorTests()
    {
        _validator = new UpdateCartItemValidator();
    }

    /// <summary>
    /// Tests that a valid UpdateCartItemCommand passes all validation rules without errors.
    /// </summary>
    [Fact(DisplayName = "Valid UpdateCartItemCommand should have no validation errors")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new UpdateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = 10.5m
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty ProductId fails validation.
    /// </summary>
    [Fact(DisplayName = "UpdateCartItemCommand with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
    {
        var command = new UpdateCartItemCommand
        {
            ProductId = Guid.Empty,
            Quantity = 1,
            UnitPrice = 10.5m
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.ProductId);
    }

    /// <summary>
    /// Tests that a Quantity less than or equal to zero fails validation.
    /// </summary>
    [Theory(DisplayName = "Quantity less than or equal to zero should fail validation")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError(int quantity)
    {
        var command = new UpdateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = quantity,
            UnitPrice = 10.5m
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Quantity);
    }

    /// <summary>
    /// Tests that a negative UnitPrice fails validation.
    /// </summary>
    [Fact(DisplayName = "Negative UnitPrice should fail validation")]
    public void Given_NegativeUnitPrice_When_Validated_Then_ShouldHaveError()
    {
        var command = new UpdateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = -5m
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.UnitPrice);
    }
}
