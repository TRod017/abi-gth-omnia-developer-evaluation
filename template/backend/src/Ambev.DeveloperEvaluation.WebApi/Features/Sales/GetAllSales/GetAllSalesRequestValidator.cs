using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Validator for <see cref="GetAllSalesRequest"/> that defines pagination rules.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Page</c> is greater than 0
/// - <c>Size</c> is between 1 and 100
/// </remarks>
public class GetAllSalesRequestValidator : AbstractValidator<GetAllSalesRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesRequestValidator"/> class
    /// and sets up pagination rules for Sale listing requests.
    /// </summary>
    public GetAllSalesRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100.");
    }
}

