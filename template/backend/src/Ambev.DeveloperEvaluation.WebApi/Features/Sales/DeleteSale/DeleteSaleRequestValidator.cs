using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Validator for <see cref="DeleteSaleRequest"/> that ensures a valid Sale ID is provided.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Id</c> is not empty
/// </remarks>
public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleRequestValidator"/> class
    /// and sets up validation rules for deleting a Sale via the API.
    /// </summary>
    public DeleteSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required.");
    }
}

