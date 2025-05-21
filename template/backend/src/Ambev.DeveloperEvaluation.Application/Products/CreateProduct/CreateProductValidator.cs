using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for <see cref="CreateProductCommand"/> that defines validation rules for product creation.
/// </summary>
/// <remarks>
/// This validator ensures that all required fields are provided and meet the business constraints:
/// - <c>Name</c> and <c>Description</c> must not be empty.
/// - <c>UnitPrice</c> and <c>AvailableQuantity</c> must be non-negative.
/// </remarks>
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductValidator"/> class
    /// with defined validation rules for creating a product.
    /// </summary>
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Product description is required.");

        RuleFor(p => p.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater.");

        RuleFor(p => p.AvailableQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Available quantity must be zero or greater.");
    }
}
