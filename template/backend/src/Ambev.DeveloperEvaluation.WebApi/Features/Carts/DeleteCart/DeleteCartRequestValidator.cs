using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Validator for <see cref="DeleteCartRequest"/> that ensures a valid cart ID is provided.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Id</c> is not empty
/// </remarks>
public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartRequestValidator"/> class
    /// and sets up validation rules for deleting a cart via the API.
    /// </summary>
    public DeleteCartRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required.");
    }
}
