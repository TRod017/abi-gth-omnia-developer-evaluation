using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Handler responsible for processing <see cref="GetAllUsersCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves all users from the repository and maps them to the
/// <see cref="GetAllUsersResult"/> response model using <see cref="IMapper"/>. It also logs
/// the operation using <see cref="ILogger"/> for observability.
/// </remarks>
public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, IReadOnlyCollection<GetAllUsersResult>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllUsersHandler> _logger;

    // EventIds
    private static readonly EventId RetrievingAllUsersEvent = new(3501, nameof(RetrievingAllUsersEvent));
    private static readonly EventId RetrievedUsersEvent = new(3502, nameof(RetrievedUsersEvent));
    private static readonly EventId GetAllUsersErrorEvent = new(3599, nameof(GetAllUsersErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Exception?> LogRetrievingAllUsers =
        LoggerMessage.Define(
            LogLevel.Information,
            RetrievingAllUsersEvent,
            "Retrieving all users");

    private static readonly Action<ILogger, int, Exception?> LogRetrievedUsers =
        LoggerMessage.Define<int>(
            LogLevel.Information,
            RetrievedUsersEvent,
            "Retrieved {Count} users");

    private static readonly Action<ILogger, Exception> LogUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            GetAllUsersErrorEvent,
            "Unexpected error while retrieving users");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllUsersHandler"/> class.
    /// </summary>
    /// <param name="repository">The user repository for data access.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public GetAllUsersHandler(IUserRepository repository, IMapper mapper, ILogger<GetAllUsersHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetAllUsersCommand"/> request and returns a list of users.
    /// </summary>
    /// <param name="command">The request command to retrieve all users.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A collection of <see cref="GetAllUsersResult"/> representing the users.</returns>
    public async Task<IReadOnlyCollection<GetAllUsersResult>> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogRetrievingAllUsers(_logger, null);

            var users = await _repository.GetAllAsync(cancellationToken);

            LogRetrievedUsers(_logger, users.Count, null);

            return _mapper.Map<IReadOnlyCollection<GetAllUsersResult>>(users);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
