using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartCommand"/> that ensures required fields are populated
/// and each cart item is valid.
/// </summary>
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
