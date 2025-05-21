using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Validator for <see cref="DeleteCartCommand"/> that ensures a valid cart ID is provided.
/// </summary>
/// <remarks>
/// This validator enforces that the cart ID is not empty before processing the delete operation.
/// </remarks>
public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartValidator"/> class
    /// with defined validation rules for deleting a cart.
    /// </summary>
    public DeleteCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");
    }
}
