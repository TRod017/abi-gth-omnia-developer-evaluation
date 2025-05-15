using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Handler responsible for processing <see cref="DeleteCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the incoming delete command using <see cref="DeleteCartValidator"/>,
/// attempts to delete the cart from the repository using <see cref="ICartRepository"/>,
/// and logs the operation using <see cref="ILogger"/>. Returns <c>true</c> if the deletion is successful,
/// or <c>false</c> if the cart was not found.
/// </remarks>
public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, bool>
{
    private readonly ICartRepository _repository;
    private readonly ILogger<DeleteCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository instance.</param>
    /// <param name="logger">The logger instance.</param>
    public DeleteCartHandler(ICartRepository repository, ILogger<DeleteCartHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="DeleteCartCommand"/> request.
    /// </summary>
    /// <param name="command">The command containing the cart ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the cart was successfully deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling DeleteCartCommand for ID: {CartId}", command.Id);

            var validator = new DeleteCartValidator();
            var validation = await validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.LogWarning("Validation failed for DeleteCartCommand. Errors: {@Errors}", validation.Errors);
                throw new ValidationException(validation.Errors);
            }

            var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

            if (deleted)
                _logger.LogInformation("Cart with ID {CartId} deleted successfully", command.Id);
            else
                _logger.LogWarning("Cart with ID {CartId} not found", command.Id);

            return deleted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while deleting cart with ID {CartId}", command.Id);
            throw;
        }
    }
}
