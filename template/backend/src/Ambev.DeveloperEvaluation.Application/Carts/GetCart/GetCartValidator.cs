using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Validator for <see cref="GetCartCommand"/> that ensures a valid cart ID is provided.
/// </summary>
/// <remarks>
/// This validator enforces that the cart ID must not be empty
/// before executing the get operation.
/// </remarks>
public class GetCartValidator : AbstractValidator<GetCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartValidator"/> class with validation rules for retrieving a cart.
    /// </summary>
    public GetCartValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");
    }
}
