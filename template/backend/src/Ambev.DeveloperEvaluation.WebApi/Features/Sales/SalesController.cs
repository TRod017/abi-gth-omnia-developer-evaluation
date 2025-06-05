using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Microsoft.AspNetCore.Authorization;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// API controller responsible for handling all Sale-related operations.
/// </summary>
/// <remarks>
/// Provides endpoints for creating, retrieving, updating, and deleting Sales.
/// Integrates with the application layer via <see cref="IMediator"/> and utilizes
/// <see cref="AutoMapper"/> and <see cref="ILogger"/> for mapping and logging respectively.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<SalesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalesController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator instance for command/query dispatch.</param>
    /// <param name="mapper">AutoMapper instance for object mappings.</param>
    /// <param name="logger">Logger instance for structured logging.</param>
    public SalesController(IMediator mediator, IMapper mapper, ILogger<SalesController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new Sale.
    /// </summary>
    /// <param name="request">Payload with user ID and Sale items.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The newly created Sale ID wrapped in an API response.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to create a new Sale");

        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Create Sale request validation failed: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<CreateSaleCommand>(request);
        command.UserId = GetAuthenticatedUserId();

        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Sale created successfully with ID: {SaleId}", result.Id);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves a Sale by its unique identifier.
    /// </summary>
    /// <param name="id">The Sale ID.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The Sale with matching ID, or not found if nonexistent.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve Sale with ID: {SaleId}", id);

        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Get Sale request validation failed for ID: {SaleId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<GetSaleCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Sale not found with ID: {SaleId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Sale with ID {id} not found"
            });
        }

        _logger.LogInformation("Sale retrieved successfully with ID: {SaleId}", id);

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<GetSaleResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves all existing Sales in the system.
    /// </summary>
    /// <param name="request">Pagination parameters passed via query string.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A paginated list of Sales.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetAllSalesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllSalesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve all Sales");

        // Validação dos parâmetros de paginação
        var validator = new GetAllSalesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid pagination parameters: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        // Mapeia o request da WebApi para o command da camada de Application
        var command = _mapper.Map<GetAllSalesCommand>(request);

        // Handler retorna já paginado via EF
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Retrieved {Count} Sales", result.TotalCount);

        return Ok(new PaginatedResponse<GetAllSalesResponse>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalCount = result.TotalCount,
            Data = _mapper.Map<IEnumerable<GetAllSalesResponse>>(result)
        });
    }



    /// <summary>
    /// Updates an existing Sale by its ID.
    /// </summary>
    /// <param name="id">The Sale ID to update.</param>
    /// <param name="request">The updated Sale information.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The updated Sale ID or 404 if not found.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to update Sale with ID: {SaleId}", id);

        request.Id = id;
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Update Sale request validation failed for ID: {SaleId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.UserId = GetAuthenticatedUserId();

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Sale not found to update with ID: {SaleId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Sale with ID {id} not found"
            });
        }

        _logger.LogInformation("Sale updated successfully with ID: {SaleId}", result.Id);

        return Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = _mapper.Map<UpdateSaleResponse>(result)
        });
    }

    /// <summary>
    /// Cancels an existing Sale by its ID.
    /// </summary>
    /// <remarks>
    /// This endpoint marks the Sale as cancelled by updating the <c>IsCancelled</c> flag to <c>true</c>.
    /// It uses a minimal payload — the Sale ID is passed via route and no request body is required.
    /// This is useful for soft-deleting or invalidating completed Sales without removing them from the system.
    /// </remarks>
    /// <param name="id">The ID of the Sale to cancel.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>HTTP 200 if successful, or 404 if not found.</returns>
    [HttpPut("{id}/cancel")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to cancel Sale with ID: {SaleId}", id);

        var command = new CancelSaleCommand
        {
            Id = id,
            IsCancelled = true,
            UserId = GetAuthenticatedUserId()
        };

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Sale not found to cancel with ID: {SaleId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Sale with ID {id} not found"
            });
        }

        _logger.LogInformation("Sale cancelled successfully with ID: {SaleId}", id);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale cancelled successfully"
        });
    }


    /// <summary>
    /// Deletes a Sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the Sale to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Status of the delete operation.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to delete Sale with ID: {SaleId}", id);

        var request = new DeleteSaleRequest { Id = id };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Delete Sale request validation failed for ID: {SaleId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<DeleteSaleCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Sale not found to delete with ID: {SaleId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Sale with ID {id} not found"
            });
        }

        _logger.LogInformation("Sale deleted successfully with ID: {SaleId}", id);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }
}

