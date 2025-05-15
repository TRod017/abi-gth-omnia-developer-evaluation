using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Validator for <see cref="GetProductRequest"/> that defines validation rules for incoming API payloads.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Id</c> is not empty
/// </remarks>
public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductRequestValidator"/> class
    /// and sets up validation rules for product retrieval via the API.
    /// </summary>
    public GetProductRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
