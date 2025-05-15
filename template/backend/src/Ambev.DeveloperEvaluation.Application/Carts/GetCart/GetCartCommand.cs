using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Query to retrieve a cart by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to encapsulate the ID required to retrieve
/// a specific cart. It returns a <see cref="GetCartResult"/> upon execution.
/// </remarks>
public class GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Validates the command using <see cref="GetCartValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing validation results such as
    /// success flag and detailed error messages, if any.
    /// </returns>
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
