using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for <see cref="GetSaleCommand"/> that ensures a valid Sale ID is provided.
/// </summary>
/// <remarks>
/// This validator enforces that the Sale ID must not be empty
/// before executing the get operation.
/// </remarks>
public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleValidator"/> class with validation rules for retrieving a Sale.
    /// </summary>
    public GetSaleValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Sale ID must be provided.");
    }
}
