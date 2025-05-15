using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

/// <summary>
/// Validator for UpdateCartItemCommand.
/// </summary>
public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
}
