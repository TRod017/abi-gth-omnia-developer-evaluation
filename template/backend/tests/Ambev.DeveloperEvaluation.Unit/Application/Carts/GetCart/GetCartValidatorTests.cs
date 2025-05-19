using FluentValidation.TestHelper;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.GetCart;

/// <summary>
/// Unit tests for the <see cref="GetCartValidator"/> class.
/// Validates that the cart ID is required and properly validated.
/// </summary>
public class GetCartValidatorTests
{
    private readonly GetCartValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartValidatorTests"/> class.
    /// </summary>
    public GetCartValidatorTests()
    {
        _validator = new GetCartValidator();
    }

    /// <summary>
    /// Tests that a valid cart ID passes validation.
    /// </summary>
    [Fact(DisplayName = "Valid Cart ID should pass validation")]
    public void Given_ValidId_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new GetCartCommand { Id = System.Guid.NewGuid() };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    /// <summary>
    /// Tests that an empty cart ID fails validation.
    /// </summary>
    [Fact(DisplayName = "Empty Cart ID should fail validation")]
    public void Given_EmptyId_When_Validated_Then_ShouldHaveError()
    {
        var command = new GetCartCommand { Id = System.Guid.Empty };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
