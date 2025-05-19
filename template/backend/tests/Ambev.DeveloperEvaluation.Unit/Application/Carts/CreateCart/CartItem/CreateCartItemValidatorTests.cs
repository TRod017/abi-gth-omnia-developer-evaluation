using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CreateCart.CartItem;

/// <summary>
/// Unit tests for the <see cref="CreateCartItemValidator"/> class.
/// These tests validate the rules defined for the CreateCartItemCommand,
/// ensuring that required fields like ProductId, Quantity, and UnitPrice
/// are correctly validated with expected constraints.
/// </summary>
public class CreateCartItemValidatorTests
{
    private readonly CreateCartItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemValidatorTests"/> class.
    /// </summary>
    public CreateCartItemValidatorTests()
    {
        _validator = new CreateCartItemValidator();
    }

    /// <summary>
    /// Tests that a valid CreateCartItemCommand passes all validation rules without errors.
    /// </summary>
    [Fact(DisplayName = "Valid CreateCartItemCommand should have no validation errors")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new CreateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = 10.5m
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that a CreateCartItemCommand with an empty ProductId fails validation.
    /// </summary>
    [Fact(DisplayName = "CreateCartItemCommand with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
    {
        var command = new CreateCartItemCommand
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
        var command = new CreateCartItemCommand
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
        var command = new CreateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = -5m
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.UnitPrice);
    }
}
