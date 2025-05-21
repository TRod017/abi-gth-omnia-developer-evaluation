using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Validator for <see cref="DeleteProductRequest"/> that defines validation rules for incoming API payloads.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Id</c> is not empty
/// </remarks>
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductRequestValidator"/> class
    /// and sets up validation rules for product deletion via the API.
    /// </summary>
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
