using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Command to delete a cart by its unique identifier.
/// </summary>
/// <remarks>
/// This command is used to request the deletion of a cart.
/// It returns a boolean indicating the success of the operation.
/// </remarks>
public class DeleteCartCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartCommand"/> class.
    /// Parameterless constructor required for deserialization and model binding.
    /// </summary>
    public DeleteCartCommand() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartCommand"/> class
    /// with the specified cart identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    public DeleteCartCommand(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Validates the command using <see cref="DeleteCartValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing validation results such as
    /// success flag and detailed error messages, if any.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new DeleteCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
}
