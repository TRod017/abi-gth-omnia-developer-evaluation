using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for <see cref="CreateProductRequest"/> that defines validation rules for incoming API payloads.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Name</c> and <c>Description</c> are not empty
/// - <c>UnitPrice</c> and <c>AvailableQuantity</c> are zero or greater
/// </remarks>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductRequestValidator"/> class
    /// and sets up validation rules for product creation via the API.
    /// </summary>
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty();
        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(0);
        RuleFor(p => p.AvailableQuantity).GreaterThanOrEqualTo(0);
    }
}
