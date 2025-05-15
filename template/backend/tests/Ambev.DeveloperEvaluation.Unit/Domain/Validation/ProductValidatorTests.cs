using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the ProductValidator class.
/// Validates rules for name, description, price, and quantity.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator = new();

    [Fact(DisplayName = "Valid product should pass all validation rules")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        var product = new Product
        {
            Name = "Notebook",
            Description = "Dell i7 16GB",
            UnitPrice = 4999.90m,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(product);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Empty or null name should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_EmptyName_When_Validated_Then_ShouldHaveError(string? name)
    {
        var product = new Product
        {
            Name = name ?? string.Empty,
            Description = "Desc",
            UnitPrice = 100,
            AvailableQuantity = 1
        };

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Theory(DisplayName = "Empty or null description should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_EmptyDescription_When_Validated_Then_ShouldHaveError(string? desc)
    {
        var product = new Product
        {
            Name = "Valid Name",
            Description = desc ?? string.Empty,
            UnitPrice = 100,
            AvailableQuantity = 1
        };

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact(DisplayName = "Negative price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldHaveError()
    {
        var product = new Product
        {
            Name = "Name",
            Description = "Desc",
            UnitPrice = -1,
            AvailableQuantity = 1
        };

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.UnitPrice);
    }

    [Fact(DisplayName = "Negative quantity should fail validation")]
    public void Given_NegativeQuantity_When_Validated_Then_ShouldHaveError()
    {
        var product = new Product
        {
            Name = "Name",
            Description = "Desc",
            UnitPrice = 10,
            AvailableQuantity = -5
        };

        var result = _validator.TestValidate(product);

        result.ShouldHaveValidationErrorFor(p => p.AvailableQuantity);
    }
}
