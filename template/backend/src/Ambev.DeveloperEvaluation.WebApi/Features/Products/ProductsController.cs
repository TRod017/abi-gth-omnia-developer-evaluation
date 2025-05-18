using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// API controller responsible for handling all product-related operations.
/// </summary>
/// <remarks>
/// Provides endpoints for creating, retrieving, updating, and deleting products.
/// Integrates with the application layer via <see cref="IMediator"/> and utilizes
/// <see cref="AutoMapper"/> and <see cref="ILogger"/> for mapping and logging respectively.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator instance for command/query dispatch.</param>
    /// <param name="mapper">AutoMapper instance for DTO conversions.</param>
    /// <param name="logger">Logger instance for structured logging.</param>
    public ProductsController(IMediator mediator, IMapper mapper, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request">The product creation payload.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A response with the created product ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to create a new product");

        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Create product request validation failed: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Product created successfully with ID: {ProductId}", result.Id);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The product details if found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve product with ID: {ProductId}", id);

        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Get product request validation failed for ID: {ProductId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<GetProductCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Product not found with ID: {ProductId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Product with ID {id} not found"
            });
        }

        _logger.LogInformation("Product retrieved successfully with ID: {ProductId}", id);

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductResponse>(result)
        });
    }

    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="request">Pagination parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetAllProductsResponse>), 200)]
    [ProducesResponseType(typeof(IEnumerable<FluentValidation.Results.ValidationFailure>), 400)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve products");

        // Validação dos parâmetros de paginação
        var validator = new GetAllProductsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid pagination parameters: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        // Mapeia o request da WebApi para o command da camada de Application
        var command = _mapper.Map<GetAllProductsCommand>(request);

        // Handler retorna já paginado via EF
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Retrieved {Count} products", result.TotalCount);

        return Ok(new PaginatedResponse<GetAllProductsResponse>
        {
            Success = true,
            Message = "Products retrieved successfully",
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalCount = result.TotalCount,
            Data = _mapper.Map<IEnumerable<GetAllProductsResponse>>(result)
        });
    }


    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="request">The updated product information.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The updated product information if successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to update product with ID: {ProductId}", id);

        request.Id = id;
        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Update product request validation failed for ID: {ProductId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<UpdateProductCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("Product not found to update with ID: {ProductId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Product with ID {id} not found"
            });
        }

        _logger.LogInformation("Product updated successfully with ID: {ProductId}", result.Id);

        return Ok(new ApiResponseWithData<UpdateProductResponse>
        {
            Success = true,
            Message = "Product updated successfully",
            Data = _mapper.Map<UpdateProductResponse>(result)
        });
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A response indicating whether the deletion was successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to delete product with ID: {ProductId}", id);

        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Delete product request validation failed for ID: {ProductId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Product not found to delete with ID: {ProductId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"Product with ID {id} not found"
            });
        }

        _logger.LogInformation("Product deleted successfully with ID: {ProductId}", id);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Product deleted successfully"
        });
    }
}
