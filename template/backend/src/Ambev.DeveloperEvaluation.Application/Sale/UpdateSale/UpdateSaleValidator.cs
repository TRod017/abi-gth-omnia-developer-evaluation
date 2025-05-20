using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.SaleItems;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for updating a Sale.
/// </summary>
/// <remarks>
/// Ensures that all required fields for a Sale update are present and valid, including:
/// - Sale ID and User ID must not be empty
/// - Each item in the Sale must be valid according to <see cref="UpdateSaleItemValidator"/>
/// </remarks>
public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleValidator"/> class
    /// and sets up validation rules for Sale updates.
    /// </summary>
    public UpdateSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID must be provided.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID must be provided.");

        RuleForEach(x => x.Items)
            .SetValidator(new UpdateSaleItemValidator());
    }
}
