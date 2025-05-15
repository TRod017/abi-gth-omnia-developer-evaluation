using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Validator for DeleteCartCommand.
/// </summary>
public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    /// <summary>
    /// Initializes the validation rules.
    /// </summary>
    public DeleteCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID must be provided.");
    }
}
