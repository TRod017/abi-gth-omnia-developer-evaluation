using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Validator for <see cref="UpdateCartCommand"/> that defines validation rules for updating a cart.
/// </summary>
/// <remarks>
/// Ensures that all required fields for a cart update are present and valid, including:
/// - Cart ID and User ID must not be empty
/// - Each item in the cart must be valid according to <see cref="UpdateCartItemValidator"/>
/// </remarks>
public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartValidator"/> class
    /// and sets up validation rules for cart updates.
    /// </summary>
    public UpdateCartValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new UpdateCartItemValidator());
    }
}
