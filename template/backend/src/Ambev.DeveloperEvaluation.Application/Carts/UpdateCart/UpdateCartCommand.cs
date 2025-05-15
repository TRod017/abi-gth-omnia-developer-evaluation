using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating an existing cart.
/// </summary>
public class UpdateCartCommand : IRequest<UpdateCartResult>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<UpdateCartItemCommand> Items { get; set; } = new();

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
