using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for <see cref="UpdateProductRequest"/> that defines validation rules for incoming API payloads.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Id</c> is not empty
/// - <c>Name</c> and <c>Description</c> are not empty
/// - <c>UnitPrice</c> and <c>AvailableQuantity</c> are zero or greater
/// </remarks>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductRequestValidator"/> class
    /// and sets up validation rules for product updates via the API.
    /// </summary>
    public UpdateProductRequestValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(0);
        RuleFor(p => p.AvailableQuantity).GreaterThanOrEqualTo(0);
    }
}
