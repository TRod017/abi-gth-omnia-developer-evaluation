using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for creating a sale.
/// </summary>
/// <remarks>
/// Ensures that the <c>SaleId</c> is provided and not empty.
/// </remarks>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleValidator"/> class with validation rules.
    /// </summary>
    public CreateSaleValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");
    }
}
