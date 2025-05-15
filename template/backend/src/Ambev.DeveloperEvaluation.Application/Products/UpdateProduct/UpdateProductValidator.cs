using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for <see cref="UpdateProductCommand"/> that defines validation rules for updating a product.
/// </summary>
/// <remarks>
/// Ensures that all required fields for a product update are present and valid, including:
/// - Product ID must not be empty
/// - Name and description must not be empty
/// - Unit price and available quantity must be greater than or equal to zero
/// </remarks>
public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductValidator"/> class
    /// and sets up validation rules for product updates.
    /// </summary>
    public UpdateProductValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");

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
