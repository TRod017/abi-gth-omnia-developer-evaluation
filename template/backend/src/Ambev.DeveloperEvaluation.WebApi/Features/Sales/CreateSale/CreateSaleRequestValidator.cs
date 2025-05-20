using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleRequest"/> that defines rules for user ID and Sale items.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>UserId</c> is not empty
/// - Each item in <c>Items</c> is individually validated by <see cref="CreateSaleItemRequestValidator"/>
/// </remarks>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidator"/> class
    /// and configures validation rules for Sale creation via the API.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for <see cref="CreateSaleItemRequest"/> that defines rules for individual Sale items.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>ProductId</c> is not empty
/// - <c>Quantity</c> is greater than 0
/// - <c>UnitPrice</c> is zero or greater
/// </remarks>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleItemRequestValidator"/> class
    /// and sets up validation rules for individual items in the Sale creation payload.
    /// </summary>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
}
