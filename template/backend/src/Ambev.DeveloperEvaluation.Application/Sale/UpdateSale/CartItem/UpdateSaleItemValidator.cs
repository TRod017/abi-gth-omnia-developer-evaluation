using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.SaleItems;

/// <summary>
/// Validator for <see cref="UpdateSaleItemCommand"/> that defines validation rules for updating a Sale item.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>ProductId</c> is not empty
/// - <c>Quantity</c> is greater than zero
/// - <c>UnitPrice</c> is zero or greater
/// </remarks>
public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleItemValidator"/> class
    /// and sets up validation rules for Sale item updates.
    /// </summary>
    public UpdateSaleItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater.");
    }
}
