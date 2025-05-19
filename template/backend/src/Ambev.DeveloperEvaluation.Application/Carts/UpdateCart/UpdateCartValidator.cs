using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;
using Ambev.DeveloperEvaluation.Domain.Enums;

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
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID must be provided.");

        RuleFor(x => x.Status)
           .NotEqual(CartStatus.Unknown)
           .WithMessage("Status cannot be Unknown.");

        RuleForEach(x => x.Items)
            .SetValidator(new UpdateCartItemValidator());
    }
}
