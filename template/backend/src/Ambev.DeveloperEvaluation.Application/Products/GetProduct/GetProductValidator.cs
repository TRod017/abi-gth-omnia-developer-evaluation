using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Validator for <see cref="GetProductCommand"/> that ensures a valid product ID is provided.
/// </summary>
/// <remarks>
/// This validator enforces that the product ID must not be empty
/// before executing the get operation.
/// </remarks>
public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductValidator"/> class
    /// with defined validation rules for retrieving a product.
    /// </summary>
    public GetProductValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");
    }
}
