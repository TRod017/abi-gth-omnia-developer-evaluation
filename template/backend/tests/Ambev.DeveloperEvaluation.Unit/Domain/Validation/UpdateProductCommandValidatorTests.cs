using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.Validation;

/// <summary>
/// Unit tests for the UpdateProductCommandValidator class.
/// </summary>
public class UpdateProductCommandValidatorTests
{
    private readonly UpdateProductValidator _validator = new();

    [Fact(DisplayName = "Update command with valid data should pass validation")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "Monitor 27\" 4K",
            Description = "Alta resolução, HDMI e DisplayPort",
            UnitPrice = 1599.99m,
            AvailableQuantity = 15
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Empty ID should fail validation")]
    public void Given_EmptyId_When_Validated_Then_ShouldHaveError()
    {
        var command = new UpdateProductCommand
        {
            Id = Guid.Empty,
            Name = "Produto",
            Description = "Descrição",
            UnitPrice = 100,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Theory(DisplayName = "Invalid name should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidName_When_Validated_Then_ShouldHaveError(string name)
    {
        var command = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = name!,
            Description = "Descrição",
            UnitPrice = 100,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Theory(DisplayName = "Invalid description should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidDescription_When_Validated_Then_ShouldHaveError(string description)
    {
        var command = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "Produto",
            Description = description!,
            UnitPrice = 100,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Fact(DisplayName = "Negative price should fail validation")]
    public void Given_NegativePrice_When_Validated_Then_ShouldHaveError()
    {
        var command = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "Produto",
            Description = "Descrição",
            UnitPrice = -10,
            AvailableQuantity = 10
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.UnitPrice);
    }

    [Fact(DisplayName = "Negative quantity should fail validation")]
    public void Given_NegativeQuantity_When_Validated_Then_ShouldHaveError()
    {
        var command = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "Produto",
            Description = "Descrição",
            UnitPrice = 100,
            AvailableQuantity = -1
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.AvailableQuantity);
    }
}
