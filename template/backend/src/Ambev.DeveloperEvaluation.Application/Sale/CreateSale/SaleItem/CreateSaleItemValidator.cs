using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

/// <summary>
/// Validator for <see cref="CreateSaleItemCommand"/> that defines rules for validating each sale item.
/// </summary>
public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleItemValidator"/> class with validation rules.
    /// </summary>
    public CreateSaleItemValidator()
    {
        RuleFor(i => i.ProductId)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(i => i.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater.");
    }
}
