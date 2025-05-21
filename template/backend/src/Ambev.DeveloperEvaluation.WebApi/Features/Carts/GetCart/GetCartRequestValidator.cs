using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Validator for <see cref="GetCartRequest"/> that ensures a valid cart ID is provided.
/// </summary>
/// <remarks>
/// Validation rule:
/// - <c>Id</c> must not be empty
/// </remarks>
public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartRequestValidator"/> class
    /// and sets up validation rules for retrieving a cart by ID via the API.
    /// </summary>
    public GetCartRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");
    }
}
