using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartCommand.
/// </summary>
public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new UpdateCartItemValidator());
    }
}
