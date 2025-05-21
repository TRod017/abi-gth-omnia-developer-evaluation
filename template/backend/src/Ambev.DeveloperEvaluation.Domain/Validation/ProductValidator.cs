using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the <see cref="Product"/> entity
/// </summary>
/// <remarks>
/// Validates required fields, price and quantity constraints
/// </remarks>
public class ProductValidator : AbstractValidator<Product>
{
    /// <summary>
    /// Initializes the validation rules for Product
    /// </summary>
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Product description is required");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater");

        RuleFor(x => x.AvailableQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Available quantity must be zero or greater");
    }
}
