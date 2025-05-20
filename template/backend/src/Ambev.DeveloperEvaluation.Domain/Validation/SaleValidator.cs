using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for <see cref="Sale"/> entity.
/// Defines rules to ensure the sale is valid before processing.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaleValidator"/> class
    /// with validation rules for sale creation and updates.
    /// </summary>
    public SaleValidator()
    {
        RuleFor(sale => sale.UserId)
            .NotEmpty()
            .WithMessage("User ID must be provided.");

        RuleFor(sale => sale.CartId)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}
