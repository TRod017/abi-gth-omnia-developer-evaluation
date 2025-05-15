using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command to delete a product by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to request the deletion of a product. 
/// It returns a boolean indicating the success of the operation.
/// </remarks>
public class DeleteProductCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new DeleteProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
