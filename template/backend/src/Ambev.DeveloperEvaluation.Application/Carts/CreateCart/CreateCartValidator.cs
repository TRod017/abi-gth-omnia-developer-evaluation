using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartCommand"/> that defines validation rules for cart creation.
/// </summary>
/// <remarks>
/// This validator ensures that all required fields are provided and meet the business constraints:
/// - <c>UserId</c> must not be empty.
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

        RuleForEach(x => x.Items)
            .SetValidator(new CreateCartItemValidator());
    }
}
