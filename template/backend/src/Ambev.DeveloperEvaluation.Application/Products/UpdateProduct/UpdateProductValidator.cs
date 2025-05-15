using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for updating a product.
/// </summary>
public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductValidator with defined validation rules.
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
