using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Validator for <see cref="DeleteProductCommand"/> that ensures a valid product ID is provided.
/// </summary>
/// <remarks>
/// This validator enforces that the product ID is not empty before processing the delete operation.
/// </remarks>
public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductValidator"/> class
    /// with defined validation rules for deleting a product.
    /// </summary>
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");
    }
}
