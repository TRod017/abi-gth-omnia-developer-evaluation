using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// API controller responsible for handling all user-related operations.
/// </summary>
/// <remarks>
/// Provides endpoints for creating, retrieving, updating, and deleting users.
/// Integrates with the application layer via <see cref="IMediator"/> and utilizes
/// <see cref="AutoMapper"/> and <see cref="ILogger"/> for mapping and logging respectively.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator instance for command/query dispatch.</param>
    /// <param name="mapper">AutoMapper instance for DTO conversions.</param>
    /// <param name="logger">Logger instance for structured logging.</param>
    public UsersController(IMediator mediator, IMapper mapper, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to create a new user");

        var validator = new CreateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Create user request validation failed: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<CreateUserCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("User created successfully with ID: {UserId}", response.Id);

        return Created(string.Empty, new ApiResponseWithData<CreateUserResponse>
        {
            Success = true,
            Message = "User created successfully",
            Data = _mapper.Map<CreateUserResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve user with ID: {UserId}", id);

        var request = new GetUserRequest { Id = id };
        var validator = new GetUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Get user request validation failed for ID: {UserId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<GetUserCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
        {
            _logger.LogWarning("User not found with ID: {UserId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"User with ID {id} not found"
            });
        }

        _logger.LogInformation("User retrieved successfully with ID: {UserId}", id);

        return Ok(new ApiResponseWithData<GetUserResponse>
        {
            Success = true,
            Message = "User retrieved successfully",
            Data = _mapper.Map<GetUserResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves all existing users in the system.
    /// </summary>
    /// <param name="request">Pagination parameters passed via query string.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A paginated list of users.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetAllUsersResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to retrieve all users");

        // Validação dos parâmetros de paginação
        var validator = new GetAllUsersRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid pagination parameters: {@Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        // Mapeia o request da WebApi para o command da camada de Application
        var command = _mapper.Map<GetAllUsersCommand>(request);

        // Handler retorna já paginado via EF
        var result = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Retrieved {Count} users", result.TotalCount);

        return Ok(new PaginatedResponse<GetAllUsersResponse>
        {
            Success = true,
            Message = "Users retrieved successfully",
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalCount = result.TotalCount,
            Data = _mapper.Map<IEnumerable<GetAllUsersResponse>>(result)
        });
    }


    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="request">The updated user information.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The updated user information if successful.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to update user with ID: {UserId}", id);

        request.Id = id;

        var validator = new UpdateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Update user request validation failed for ID: {UserId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<UpdateUserCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            _logger.LogWarning("User not found to update with ID: {UserId}", id);
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = $"User with ID {id} not found"
            });
        }

        _logger.LogInformation("User updated successfully with ID: {UserId}", result.Id);

        return Ok(new ApiResponseWithData<UpdateUserResponse>
        {
            Success = true,
            Message = "User updated successfully",
            Data = _mapper.Map<UpdateUserResponse>(result)
        });
    }

    /// <summary>
    /// Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to delete user with ID: {UserId}", id);

        var request = new DeleteUserRequest { Id = id };
        var validator = new DeleteUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Delete user request validation failed for ID: {UserId}", id);
            return BadRequest(validationResult.Errors);
        }

        var command = _mapper.Map<DeleteUserCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("User deleted successfully with ID: {UserId}", id);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "User deleted successfully"
        });
    }
}
