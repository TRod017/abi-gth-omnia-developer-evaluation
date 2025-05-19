using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.UpdateCart;

/// <summary>
/// Unit tests for the <see cref="UpdateCartValidator"/> class.
/// Covers validation rules for:
/// - Cart ID must not be empty,
/// - User ID must not be empty,
/// - Each item must be validated by UpdateCartItemValidator.
/// </summary>
public class UpdateCartValidatorTests
{
    private readonly UpdateCartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the validator tests.
    /// </summary>
    public UpdateCartValidatorTests()
    {
        _validator = new UpdateCartValidator();
    }

    /// <summary>
    /// Tests that a valid command passes validation without errors.
    /// </summary>
    [Fact(DisplayName = "Valid UpdateCartCommand should pass validation")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        command.Status = CartStatus.Open;

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that an empty Cart ID triggers a validation error.
    /// </summary>
    [Fact(DisplayName = "Empty Cart ID should fail validation")]
    public void Given_EmptyId_When_Validated_Then_ShouldHaveError()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        command.Id = System.Guid.Empty; // Força Id inválido

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    /// <summary>
    /// Tests that an empty User ID triggers a validation error.
    /// </summary>
    [Fact(DisplayName = "Empty User ID should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        command.UserId = System.Guid.Empty; // Força UserId inválido

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    /// <summary>
    /// Tests that invalid items trigger validation errors.
    /// </summary>
    [Fact(DisplayName = "Invalid item in Items list should fail validation")]
    public void Given_InvalidItemInItems_When_Validated_Then_ShouldHaveError()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        // Força item inválido
        command.Items[0].ProductId = System.Guid.Empty;
        command.Items[0].Quantity = 0;
        command.Items[0].UnitPrice = -1m;

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor("Items[0].ProductId");
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        result.ShouldHaveValidationErrorFor("Items[0].UnitPrice");
    }
}
