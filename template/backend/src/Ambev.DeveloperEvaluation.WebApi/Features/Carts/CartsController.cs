using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// API controller responsible for handling all cart-related operations.
/// </summary>
/// <remarks>
/// Provides endpoints for creating, retrieving, updating, and deleting carts.
/// Integrates with the application layer via <see cref="IMediator"/> and utilizes
/// <see cref="AutoMapper"/> and <see cref="ILogger"/> for mapping and logging respectively.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CartsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartsController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator instance for command/query dispatch.</param>
    /// <param name="mapper">AutoMapper instance for object mappings.</param>
    /// <param name="logger">Logger instance for structured logging.</param>
    public CartsController(IMediator mediator, IMapper mapper, ILogger<CartsController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new cart.
    /// </summary>
    /// <param name="request">Payload with user ID and cart items.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The newly created cart ID wrapped in an API response.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to create a new cart");

        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Create cart request validation failed: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<CreateCartCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Cart created successfully with ID: {CartId}", result.Id);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = _mapper.Map<CreateCartResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves a cart by its unique identifier.
    /// </summary>
    /// <param name="id">The cart ID.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The cart with matching ID, or not found if nonexistent.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve cart with ID: {CartId}", id);

        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Get cart request validation failed for ID: {CartId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<GetCartCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Cart not found with ID: {CartId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Cart with ID {id} not found"
            });
        }

        _logger.LogInformation("Cart retrieved successfully with ID: {CartId}", id);

        return Ok(new ApiResponseWithData<GetCartResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = _mapper.Map<GetCartResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves all existing carts in the system.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A list of all carts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetAllCartsResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve all carts");

        var result = await _mediator.Send(new GetAllCartsCommand(), cancellationToken);

        _logger.LogInformation("Retrieved {Count} carts", result.Count);

        return Ok(new ApiResponseWithData<IEnumerable<GetAllCartsResponse>>
        {
            Success = true,
            Message = "Carts retrieved successfully",
            Data = _mapper.Map<IEnumerable<GetAllCartsResponse>>(result)
        });
    }

    /// <summary>
    /// Updates an existing cart by its ID.
    /// </summary>
    /// <param name="id">The cart ID to update.</param>
    /// <param name="request">The updated cart information.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The updated cart ID or 404 if not found.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to update cart with ID: {CartId}", id);

        request.Id = id;
        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Update cart request validation failed for ID: {CartId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<UpdateCartCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Cart not found to update with ID: {CartId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Cart with ID {id} not found"
            });
        }

        _logger.LogInformation("Cart updated successfully with ID: {CartId}", result.Id);

        return Ok(new ApiResponseWithData<UpdateCartResponse>
        {
            Success = true,
            Message = "Cart updated successfully",
            Data = _mapper.Map<UpdateCartResponse>(result)
        });
    }

    /// <summary>
    /// Deletes a cart by its ID.
    /// </summary>
    /// <param name="id">The ID of the cart to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status of the delete operation.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to delete cart with ID: {CartId}", id);

        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Delete cart request validation failed for ID: {CartId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<DeleteCartCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Cart not found to delete with ID: {CartId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Cart with ID {id} not found"
            });
        }

        _logger.LogInformation("Cart deleted successfully with ID: {CartId}", id);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart deleted successfully"
        });
    }
}
