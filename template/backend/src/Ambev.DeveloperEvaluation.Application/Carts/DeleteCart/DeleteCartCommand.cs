using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Command to delete a cart by its unique identifier.
/// </summary>
/// <remarks>
/// This command encapsulates the ID of the cart to be deleted.
/// </remarks>
public class DeleteCartCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Validates the command input.
    /// </summary>
    public ValidationResultDetail Validate()
    {
        var validator = new DeleteCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}