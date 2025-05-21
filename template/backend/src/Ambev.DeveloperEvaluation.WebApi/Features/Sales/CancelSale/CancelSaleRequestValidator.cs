using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Validator for <see cref="CancelSaleRequest"/> that ensures a valid Sale cancellation payload.
/// </summary>
/// <remarks>
/// Validations applied:
/// - <c>Id</c> must not be empty
/// - <c>UserId</c> must not be empty
/// </remarks>
public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleRequestValidator"/> class
    /// and sets up validation rules for cancelling a Sale via the API.
    /// </summary>
    public CancelSaleRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
