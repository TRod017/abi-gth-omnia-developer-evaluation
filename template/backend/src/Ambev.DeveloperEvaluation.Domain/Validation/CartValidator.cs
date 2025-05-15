using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for <see cref="Cart"/> entity.
/// Defines rules to ensure the cart is valid before processing.
/// </summary>
public class CartValidator : AbstractValidator<Cart>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CartValidator"/> class
    /// with validation rules for cart creation and updates.
    /// </summary>
    public CartValidator()
    {
        RuleFor(cart => cart.UserId)
            .NotEmpty()
            .WithMessage("User ID must be provided.");

        RuleFor(cart => cart.Status)
            .IsInEnum()
            .NotEqual(CartStatus.Unknown)
            .WithMessage("Cart status must be a valid value.");

        RuleForEach(cart => cart.Items)
            .SetValidator(new CartItemValidator());
    }
}
