using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartCommand"/> that defines validation rules for cart creation.
/// </summary>
/// <remarks>
/// This validator ensures that all required fields are provided and meet the business constraints:
/// - <c>UserId</c> must not be empty.
/// - <c>Items</c> must not be null or empty.
/// - Each item in <c>Items</c> must be valid according to <see cref="CreateCartItemValidator"/>.
/// </remarks>
public class CreateCartValidator : AbstractValidator<CreateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartValidator"/> class with validation rules.
    /// </summary>
    public CreateCartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID must be provided.");
        
        RuleFor(x => x.Status)
                .NotEqual(CartStatus.Unknown)
                .WithMessage("Cart status must be a valid value.");

        RuleFor(x => x.Items)
            .NotNull()
            .WithMessage("Cart items must be provided.")
            .NotEmpty()
            .WithMessage("Cart must contain at least one item.");

        RuleForEach(x => x.Items)
            .SetValidator(new CreateCartItemValidator());
    }
}
