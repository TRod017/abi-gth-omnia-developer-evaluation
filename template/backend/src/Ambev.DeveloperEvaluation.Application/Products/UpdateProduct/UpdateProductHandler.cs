using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications.Product;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler responsible for processing <see cref="UpdateProductCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler relies on MediatR's pipeline behavior to automatically perform validation using
/// registered FluentValidation validators, so explicit validation calls in this handler are removed.
/// It retrieves the existing product from the repository, applies updates, and persists the entity.
/// All steps are logged using <see cref="ILogger"/>. Returns <see cref="UpdateProductResult"/> on success,
/// or null if the product was not found.
/// </remarks>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductHandler> _logger;

    // EventIds
    private static readonly EventId StartUpdateEvent = new(3201, nameof(StartUpdateEvent));
    private static readonly EventId ProductNotFoundEvent = new(3203, nameof(ProductNotFoundEvent));
    private static readonly EventId ProductUpdatedEvent = new(3204, nameof(ProductUpdatedEvent));
    private static readonly EventId UnexpectedErrorEvent = new(3299, nameof(UnexpectedErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogStartUpdate =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            StartUpdateEvent,
            "Handling UpdateProductCommand for ID: {ProductId}");

    private static readonly Action<ILogger, Guid, Exception?> LogProductNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            ProductNotFoundEvent,
            "Product with ID {ProductId} not found");

    private static readonly Action<ILogger, Guid, Exception?> LogProductUpdated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            ProductUpdatedEvent,
            "Product with ID {ProductId} updated successfully");

    private static readonly Action<ILogger, Exception> LogUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            UnexpectedErrorEvent,
            "Unexpected error while updating product");

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductHandler"/> class.
    /// </summary>
    /// <param name="repository">The product repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="logger">The logger instance.</param>
    public UpdateProductHandler(IProductRepository repository, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateProductCommand"/> and returns the updated product.
    /// </summary>
    /// <param name="command">The command containing updated product information.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>The updated product result as <see cref="UpdateProductResult"/>, or null if not found.</returns>
    public async Task<UpdateProductResult?> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogStartUpdate(_logger, command.Id, null);

            var product = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (product == null)
            {
                LogProductNotFound(_logger, command.Id, null);
                return null;
            }

            _mapper.Map(command, product);

            SpecificationValidator.Validate(product,
                (new ProductMustHaveValidPriceSpecification(), $"Product '{product.Name}' must have a valid price"));

            // Validate business rules in domain entity
            product.EnsureBusinessRulesAreMet();

            var updated = await _repository.UpdateAsync(product, cancellationToken);

            LogProductUpdated(_logger, updated.Id, null);

            return _mapper.Map<UpdateProductResult>(updated);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
