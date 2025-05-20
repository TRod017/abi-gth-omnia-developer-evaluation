using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Validator for <see cref="DeleteSaleCommand"/> that ensures a valid Sale ID is provided.
/// </summary>
/// <remarks>
/// This validator enforces that the Sale ID is not empty before processing the delete operation.
/// </remarks>
public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleValidator"/> class
    /// with defined validation rules for deleting a Sale.
    /// </summary>
    public DeleteSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID must be provided.");
    }
}
