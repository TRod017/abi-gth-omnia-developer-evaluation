using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using FluentValidation.TestHelper;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="UpdateCartCommand"/> class.
/// Validates properties and overall command correctness.
/// </summary>
public class UpdateCartCommandTests
{
    private readonly UpdateCartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartCommandTests"/> class.
    /// </summary>
    public UpdateCartCommandTests()
    {
        _validator = new UpdateCartValidator();
    }

    /// <summary>
    /// Tests that a valid command passes all validation rules.
    /// </summary>
    [Fact(DisplayName = "Valid UpdateCartCommand should pass validation")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that a command with empty Id fails validation.
    /// </summary>
    [Fact(DisplayName = "UpdateCartCommand with empty Id should fail validation")]
    public void Given_EmptyId_When_Validated_Then_ShouldHaveError()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        command.Id = Guid.Empty;

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    /// <summary>
    /// Tests that a command with empty UserId fails validation.
    /// </summary>
    [Fact(DisplayName = "UpdateCartCommand with empty UserId should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();
        command.UserId = Guid.Empty;

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    /// <summary>
    /// Tests that a command with Unknown status fails validation.
    /// </summary>
    [Fact(DisplayName = "UpdateCartCommand with Unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        command.Status = CartStatus.Unknown;

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Status);
    }
}
