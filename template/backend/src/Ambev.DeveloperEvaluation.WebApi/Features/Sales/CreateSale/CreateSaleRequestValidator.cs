using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleRequest"/> that defines rules for user ID and Sale items.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>CartId</c> is not empty
/// - <c>Branch</c> is not null or empty
/// </remarks>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidator"/> class
    /// and configures validation rules for Sale creation via the API.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId is required.");

        RuleFor(x => x.Branch)
            .NotEmpty()
            .WithMessage("Branch is required.");
    }
}
