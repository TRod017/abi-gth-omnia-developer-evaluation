using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleRequest"/> that ensures a valid Sale update payload.
/// </summary>
/// <remarks>
/// Validations applied:
/// - <c>Id</c> must not be empty
/// - <c>Status</c> must not be empty
/// - Each item in <c>Items</c> must have:
///   - <c>ProductId</c> not empty
///   - <c>Quantity</c> greater than 0
///   - <c>UnitPrice</c> greater than or equal to 0
/// </remarks>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleRequestValidator"/> class
    /// and sets up validation rules for updating a Sale via the API.
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductId).NotEmpty();
            item.RuleFor(i => i.Quantity).GreaterThan(0);
            item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}

