using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Query to retrieve a product by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the ID required to retrieve
/// a specific product. It returns a <see cref="GetProductResult"/> upon execution.
/// </remarks>
public class GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new GetProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
