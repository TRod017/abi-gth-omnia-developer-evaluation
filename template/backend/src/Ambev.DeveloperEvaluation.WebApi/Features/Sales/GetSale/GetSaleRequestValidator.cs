using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for <see cref="GetSaleRequest"/> that ensures a valid Sale ID is provided.
/// </summary>
/// <remarks>
/// Validation rule:
/// - <c>Id</c> must not be empty
/// </remarks>
public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleRequestValidator"/> class
    /// and sets up validation rules for retrieving a Sale by ID via the API.
    /// </summary>
    public GetSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID must be provided.");
    }
}

