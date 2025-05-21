using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation.TestHelper;
using Xunit;


namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="CreateCartCommand"/> class.
/// Validates properties and the overall command model correctness.
/// </summary>
public class CreateCartCommandTests
{
    private readonly CreateCartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartCommandTests"/> class.
    /// </summary>
    public CreateCartCommandTests()
    {
        _validator = new CreateCartValidator();
    }

    /// <summary>
    /// Tests that a valid command passes all validation rules.
    /// </summary>
    [Fact(DisplayName = "Valid CreateCartCommand should pass validation")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = CreateCartHandlerTestData.GenerateValidCommand();

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that a command with empty UserId fails validation.
    /// </summary>
    [Fact(DisplayName = "CreateCartCommand with empty UserId should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var command = CreateCartHandlerTestData.GenerateValidCommand();
        command.UserId = Guid.Empty;

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    /// <summary>
    /// Tests that a command with invalid CartStatus fails validation.
    /// </summary>
    [Fact(DisplayName = "CreateCartCommand with Unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError()
    {
        var command = CreateCartHandlerTestData.GenerateValidCommand();

        command.Status = CartStatus.Unknown;

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Status);
    }
}
