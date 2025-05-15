using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Validator for GetCartCommand that ensures a valid ID is provided.
/// </summary>
public class GetCartValidator : AbstractValidator<GetCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCartValidator"/> class with validation rules.
    /// </summary>
    public GetCartValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");
    }
}
