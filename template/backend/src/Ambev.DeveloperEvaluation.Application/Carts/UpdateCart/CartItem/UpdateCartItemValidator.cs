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
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater.");
    }
}
