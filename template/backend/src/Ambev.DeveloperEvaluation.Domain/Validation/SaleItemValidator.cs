using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the <see cref="SaleItem"/> entity.
/// Defines validation rules to ensure consistency and integrity of sale item data.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItemValidator"/> class
    /// with defined validation rules for <see cref="SaleItem"/>.
    /// </summary>
    public SaleItemValidator()
    {
        RuleFor(item => item.SaleId)
            .NotEmpty()
            .WithMessage("SaleId is required.");

        RuleFor(item => item.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(item => item.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(item => item.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity cannot exceed 20 units per item.");

        RuleFor(item => item)
            .Must(item => item.Quantity >= 4 || item.Discount == 0)
            .WithMessage("Items with quantity less than 4 cannot receive discount.");
    }
}
