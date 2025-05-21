using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="SaleItemValidator"/> class.
/// 
/// These tests validate the data integrity of the <see cref="SaleItem"/> entity,
/// covering scenarios including:
/// - ProductId must not be empty,
/// - Quantity must be greater than zero,
/// - Unit price must not be negative.
/// 
/// The tests leverage FluentValidation and FluentValidation.TestHelper
/// for validating rules and simplifying assertions.
/// </summary>
public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItemValidatorTests"/> class.
    /// </summary>
    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    /// <summary>
    /// Tests that a sale item with valid data passes validation without errors.
    /// </summary>
    [Fact(DisplayName = "SaleItem with valid data should pass validation")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        var item = SaleItemTestData.GenerateValidSaleItem();
        var result = _validator.TestValidate(item);
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the ProductId is empty.
    /// </summary>
    [Fact(DisplayName = "SaleItem with empty ProductId should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
    {
        var item = SaleItemTestData.GenerateWithEmptyProductId();
        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(i => i.ProductId);
    }

    /// <summary>
    /// Tests that validation fails when the Quantity is zero.
    /// </summary>
    [Fact(DisplayName = "SaleItem with zero quantity should fail validation")]
    public void Given_ZeroQuantity_When_Validated_Then_ShouldHaveError()
    {
        var item = SaleItemTestData.GenerateWithZeroQuantity();
        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(i => i.Quantity);
    }

    /// <summary>
    /// Tests that validation fails when the UnitPrice is negative.
    /// </summary>
    [Fact(DisplayName = "SaleItem with negative price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldHaveError()
    {
        var item = SaleItemTestData.GenerateWithNegativeUnitPrice();
        var result = _validator.TestValidate(item);
        result.ShouldHaveValidationErrorFor(i => i.UnitPrice);
    }
}
