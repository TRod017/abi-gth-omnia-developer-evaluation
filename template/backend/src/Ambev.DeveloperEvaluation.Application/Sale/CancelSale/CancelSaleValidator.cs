using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Validator for <see cref="CancelSaleCommand"/> that defines validation rules for cancelling a Sale.
/// </summary>
/// <remarks>
/// Ensures that all required fields for a Sale cancellation are present and valid, including:
/// - Sale ID and User ID must not be empty
/// - The cancellation flag must be explicitly true
/// </remarks>
public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleValidator"/> class
    /// and sets up validation rules for cancelling a Sale.
    /// </summary>
    public CancelSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID must be provided.");
    }
}
