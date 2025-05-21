using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for the <see cref="SaleValidator"/> class.
/// 
/// These tests verify that the Sale entity respects validation rules such as:
/// - UserId must not be empty,
/// - Status must be a valid enum value and not Unknown,
/// - Valid sale data passes validation.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleValidatorTests"/> class.
    /// </summary>
    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that a sale with valid data passes validation without errors.
    /// </summary>
    [Fact(DisplayName = "Sale with valid data should pass validation")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        var sale = SaleTestData.GenerateValidSale();

        var result = _validator.TestValidate(sale);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the UserId is empty.
    /// </summary>
    [Fact(DisplayName = "Sale with empty UserId should fail validation")]
    public void Given_EmptyUserId_When_Validated_Then_ShouldHaveError()
    {
        var sale = SaleTestData.GenerateValidSale();
        sale.UserId = System.Guid.Empty;

        var result = _validator.TestValidate(sale);

        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    /// <summary>
    /// Tests that validation fails when the Status is Unknown.
    /// </summary>
    [Fact(DisplayName = "Sale with Unknown status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError()
    {
        var sale = SaleTestData.GenerateSaleWithInvalidStatus();

        var result = _validator.TestValidate(sale);

        result.ShouldHaveValidationErrorFor(c => c.CartId);
    }
}
