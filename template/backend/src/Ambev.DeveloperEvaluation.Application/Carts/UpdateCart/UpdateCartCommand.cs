using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating an existing cart.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the updated details of a cart,
/// including the associated user and items. It returns a <see cref="UpdateCartResult"/> upon execution.
/// </remarks>
public class UpdateCartCommand : IRequest<UpdateCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the list of items to be updated in the cart.
    /// </summary>
    public List<UpdateCartItemCommand> Items { get; set; } = new();

    /// <summary>
    /// Validates the command using <see cref="UpdateCartValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing validation results such as
    /// success flag and detailed error messages, if any.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
