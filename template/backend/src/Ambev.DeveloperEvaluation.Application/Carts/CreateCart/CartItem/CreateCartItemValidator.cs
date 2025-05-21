using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;

/// <summary>
/// Validator for <see cref="CreateCartItemCommand"/> that defines rules for validating each cart item.
/// </summary>
public class CreateCartItemValidator : AbstractValidator<CreateCartItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemValidator"/> class with validation rules.
    /// </summary>
    public CreateCartItemValidator()
    {
        RuleFor(i => i.ProductId)
            .NotEmpty()
            .WithMessage("Product ID must be provided.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(i => i.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price must be zero or greater.");
    }
}
