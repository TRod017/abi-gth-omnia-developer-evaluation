using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Unit tests for the ProductValidator class.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator = new();

    [Fact(DisplayName = "Product with valid data should pass validation")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = new Product
        {
            Name = "Teclado Mecânico",
            Description = "Switch azul com RGB",
            UnitPrice = 249.90m,
            AvailableQuantity = 25
        };

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid product name should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidName_When_Validated_Then_ShouldHaveError(string name)
    {
        var product = new Product
        {
            Name = name!,
            Description = "Descrição válida",
            UnitPrice = 100.0m,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Theory(DisplayName = "Invalid product description should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        var product = new Product
        {
            Name = "Produto válido",
            Description = description!,
            UnitPrice = 100.0m,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact(DisplayName = "Negative unit price should fail validation")]
    public void Given_NegativeUnitPrice_When_Validated_Then_ShouldHaveError()
    {
        var product = new Product
        {
            Name = "Produto válido",
            Description = "Descrição válida",
            UnitPrice = -1m,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.UnitPrice);
    }

    [Fact(DisplayName = "Negative quantity should fail validation")]
    public void Given_NegativeAvailableQuantity_When_Validated_Then_ShouldHaveError()
    {
        var product = new Product
        {
            Name = "Produto válido",
            Description = "Descrição válida",
            UnitPrice = 99.99m,
            AvailableQuantity = -5
        };

        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.AvailableQuantity);
    }
}
