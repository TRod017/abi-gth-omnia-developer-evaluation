using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler responsible for processing <see cref="CreateCartCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler validates the incoming cart creation command using <see cref="CreateCartValidator"/>.
/// It maps the command to a <see cref="Cart"/> entity using <see cref="IMapper"/>,
/// persists it using <see cref="ICartRepository"/>, and logs each step of the operation
/// using <see cref="ILogger"/>. Returns a <see cref="CreateCartResult"/> upon successful creation.
/// </remarks>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCartHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository for persistence operations.</param>
    /// <param name="mapper">The AutoMapper instance used for object mapping.</param>
    /// <param name="logger">The logger used to log operational steps.</param>
    public CreateCartHandler(ICartRepository repository, IMapper mapper, ILogger<CreateCartHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateCartCommand"/> request and returns the created cart result.
    /// </summary>
    /// <param name="command">The command containing the cart data to be created.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>The result of the created cart as <see cref="CreateCartResult"/>.</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation(new EventId(1001, nameof(CreateCartHandler)), "Handling CreateCartCommand for UserId: {UserId}", command.UserId);

        var validator = new CreateCartValidator();
        var validation = await validator.ValidateAsync(command, cancellationToken);

        if (!validation.IsValid)
        {
            _logger.LogWarning(new EventId(1400, "ValidationError"), "Validation failed for CreateCartCommand. Errors: {@Errors}", validation.Errors);
            throw new ValidationException(validation.Errors);
        }

        try
        {
            var cart = _mapper.Map<Cart>(command);

            _logger.LogDebug(new EventId(1002, "Mapping"), "Mapped CreateCartCommand to Cart entity: {@Cart}", cart);

            var created = await _repository.CreateAsync(cart, cancellationToken);

            _logger.LogInformation(new EventId(1003, "Persistence"), "Cart created with ID: {CartId}", created.Id);

            return _mapper.Map<CreateCartResult>(created);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(new EventId(1500, "UnhandledException"), ex, "Unhandled exception occurred while creating cart for UserId: {UserId}", command.UserId);
            throw;
        }
    }
}
