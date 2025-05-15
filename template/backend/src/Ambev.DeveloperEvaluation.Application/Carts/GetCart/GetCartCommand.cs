using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Command to retrieve a cart by its unique identifier.
/// </summary>
public class GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new GetCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
