using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Handler responsible for processing <see cref="DeleteCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the command, attempts to delete the cart by ID using the <see cref="ICartRepository"/>.
/// Logs key steps and returns true if successful, or false otherwise.
/// </remarks>
public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, bool>
{
    private readonly ICartRepository _repository;
    private readonly ILogger<DeleteCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCartHandler"/> class.
    /// </summary>
    public DeleteCartHandler(ICartRepository repository, ILogger<DeleteCartHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="DeleteCartCommand"/> request.
    /// </summary>
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