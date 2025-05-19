using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for the <see cref="ProductValidator"/> class.
/// 
/// These tests verify that the Product entity respects validation rules such as:
/// - Name and Description must not be empty,
/// - UnitPrice and AvailableQuantity must be zero or greater,
/// - Valid product data passes validation.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductValidatorTests"/> class.
    /// </summary>
    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    /// <summary>
    /// Tests that a product with valid data passes validation without errors.
    /// </summary>
    [Fact(DisplayName = "Product with valid data should pass validation")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        var product = ProductTestData.GenerateValidProduct();

        var result = _validator.TestValidate(product);

        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when the product name is null or empty.
    /// </summary>
    /// <param name="name">Invalid product name to test.</param>
    [Theory(DisplayName = "Invalid product name should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidName_When_Validated_Then_ShouldHaveError(string name)
    {
        var product = ProductTestData.GenerateValidProduct();
        product.Name = name;

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    /// <summary>
    /// Tests that validation fails when the product description is null or empty.
    /// </summary>
    /// <param name="description">Invalid product description to test.</param>
    [Theory(DisplayName = "Invalid product description should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        var product = ProductTestData.GenerateValidProduct();
        product.Description = description;

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Description);
    }

    /// <summary>
    /// Tests that validation fails when the product's unit price is negative.
    /// </summary>
    [Fact(DisplayName = "Negative unit price should fail validation")]
    public void Given_NegativeUnitPrice_When_Validated_Then_ShouldHaveError()
    {
        var product = ProductTestData.GenerateValidProduct();
        product.UnitPrice = -1m;

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.UnitPrice);
    }

    /// <summary>
    /// Tests that validation fails when the product's available quantity is negative.
    /// </summary>
    [Fact(DisplayName = "Negative quantity should fail validation")]
    public void Given_NegativeAvailableQuantity_When_Validated_Then_ShouldHaveError()
    {
        var product = ProductTestData.GenerateValidProduct();
        product.AvailableQuantity = -5;

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.AvailableQuantity);
    }
}
