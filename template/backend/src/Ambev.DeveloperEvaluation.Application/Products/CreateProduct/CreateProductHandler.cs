using AutoMapper;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductHandler> _logger;

    private static readonly Action<ILogger, string, Exception?> _logStart =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(2000, nameof(Handle)),
            "Handling CreateProductCommand for Name: {Name}");

    private static readonly Action<ILogger, Guid, Exception?> _logCreated =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            new EventId(2001, nameof(Handle)),
            "Product created with ID: {ProductId}");

    private static readonly Action<ILogger, Exception?> _logUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2500, nameof(Handle)),
            "Unexpected error occurred while handling CreateProductCommand");

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandler"/> class.
    /// </summary>
    /// <param name="repository">The product repository for persistence operations.</param>
    /// <param name="mapper">The AutoMapper instance used for object mapping.</param>
    /// <param name="logger">The logger used to log operational steps.</param>
    public CreateProductHandler(IProductRepository repository, IMapper mapper, ILogger<CreateProductHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateProductCommand"/> request and returns the created product result.
    /// </summary>
    /// <param name="command">The command containing the product data to be created.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The result of the created product as <see cref="CreateProductResult"/>.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        _logStart(_logger, command.Name, null);

        try
        {
            var product = _mapper.Map<Product>(command);

            _logger.LogDebug("Mapped CreateProductCommand to Product entity: {@Product}", product);

            // Validate business rules in domain entity
            product.EnsureBusinessRulesAreMet();

            var created = await _repository.CreateAsync(product, cancellationToken);

            _logCreated(_logger, created.Id, null);

            return _mapper.Map<CreateProductResult>(created);
        }
        catch (Exception ex)
        {
            _logUnexpectedError(_logger, ex);
            throw;
        }
    }
}
