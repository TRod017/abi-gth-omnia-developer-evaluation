using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

/// <summary>
/// Validator for <see cref="UpdateCartItemCommand"/> that defines validation rules for updating a cart item.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>ProductId</c> is not empty
/// - <c>Quantity</c> is greater than zero
/// - <c>UnitPrice</c> is zero or greater
/// </remarks>
public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemValidator"/> class
    /// and sets up validation rules for cart item updates.
    /// </summary>
    public UpdateCartItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
}
