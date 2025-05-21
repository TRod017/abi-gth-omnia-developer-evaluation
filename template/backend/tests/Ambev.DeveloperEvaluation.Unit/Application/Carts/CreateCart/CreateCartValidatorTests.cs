using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="CreateCartValidator"/> class.
/// Covers validation rules for:
/// - UserId must not be empty,
/// - Items list must be present and not empty,
/// - Each item must pass its own validation rules.
/// </summary>
public class CreateCartValidatorTests
{
    private readonly CreateCartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the validator tests.
    /// </summary>
    public CreateCartValidatorTests()
    {
        _validator = new CreateCartValidator();
    }

    /// <summary>
    /// Tests that a valid command passes validation without errors.
    /// </summary>
    [Fact(DisplayName = "Valid command should pass validation")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items = new List<CreateCartItemCommand>
            {
                new CreateCartItemCommand { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty UserId triggers a validation error.
    /// </summary>
    [Fact(DisplayName = "Empty UserId should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var command = new CreateCartCommand
        {
            UserId = Guid.Empty,
            Items = new List<CreateCartItemCommand>
            {
                new CreateCartItemCommand { ProductId = Guid.NewGuid(), Quantity = 1 }
            }
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    /// <summary>
    /// Tests that a null Items list triggers a validation error.
    /// </summary>
    [Fact(DisplayName = "Null Items should fail validation")]
    public void Given_NullItems_When_Validated_Then_ShouldHaveError()
    {
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items = null!
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    /// <summary>
    /// Tests that an empty Items list triggers a validation error.
    /// </summary>
    [Fact(DisplayName = "Empty Items list should fail validation")]
    public void Given_EmptyItems_When_Validated_Then_ShouldHaveError()
    {
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items = new List<CreateCartItemCommand>()
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    /// <summary>
    /// Tests that invalid items in the Items list trigger validation errors via nested validators.
    /// </summary>
    [Fact(DisplayName = "Invalid item in Items list should fail validation")]
    public void Given_InvalidItemInItems_When_Validated_Then_ShouldHaveError()
    {
        var command = new CreateCartCommand
        {
            UserId = Guid.NewGuid(),
            Items = new List<CreateCartItemCommand>
            {
                new CreateCartItemCommand { ProductId = Guid.Empty, Quantity = 0 }
            }
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor("Items[0].ProductId");
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }
}
