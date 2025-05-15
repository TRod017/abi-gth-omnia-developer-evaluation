using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Validator for DeleteProductCommand that ensures a valid ID is provided.
/// </summary>
public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the DeleteProductValidator with defined validation rules.
    /// </summary>
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");
    }
}
