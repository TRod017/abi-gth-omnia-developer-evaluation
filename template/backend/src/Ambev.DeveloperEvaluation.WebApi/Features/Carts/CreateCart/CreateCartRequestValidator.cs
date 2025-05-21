using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartRequest"/> that defines rules for user ID and cart items.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>UserId</c> is not empty
/// - Each item in <c>Items</c> is individually validated by <see cref="CreateCartItemRequestValidator"/>
/// </remarks>
public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartRequestValidator"/> class
    /// and configures validation rules for cart creation via the API.
    /// </summary>
    public CreateCartRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleForEach(x => x.Items).SetValidator(new CreateCartItemRequestValidator());
    }
}

/// <summary>
/// Validator for <see cref="CreateCartItemRequest"/> that defines rules for individual cart items.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>ProductId</c> is not empty
/// - <c>Quantity</c> is greater than 0
/// - <c>UnitPrice</c> is zero or greater
/// </remarks>
public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartItemRequestValidator"/> class
    /// and sets up validation rules for individual items in the cart creation payload.
    /// </summary>
    public CreateCartItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
}
