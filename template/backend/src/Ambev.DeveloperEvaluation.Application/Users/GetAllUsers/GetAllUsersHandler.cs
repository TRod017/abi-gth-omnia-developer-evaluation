using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Handler responsible for processing <see cref="GetAllUsersCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves all users from the repository and maps them to the
/// <see cref="GetAllUsersResult"/> response model using <see cref="IMapper"/>. It also logs
/// the operation using <see cref="ILogger"/> for observability.
/// </remarks>
public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, PaginatedList<GetAllUsersResult>>
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
    /// Handles the <see cref="GetAllUsersCommand"/> request and returns a paginated list of users.
    /// </summary>
    /// <param name="command">The request command with pagination parameters.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A paginated collection of <see cref="GetAllUsersResult"/>.</returns>
    public async Task<PaginatedList<GetAllUsersResult>> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogRetrievingAllUsers(_logger, null);

            // Cria consulta paginada com EF usando o repositório
            var query = _repository.Query();
            var paginated = await PaginatedList<User>.CreateAsync(query, command.Page, command.Size, cancellationToken);

            LogRetrievedUsers(_logger, paginated.TotalCount, null);

            // Mapeia a lista de entidades User para GetAllUsersResult
            var mapped = _mapper.Map<List<GetAllUsersResult>>(paginated);

            // Retorna nova instância paginada com os resultados mapeados
            return new PaginatedList<GetAllUsersResult>(mapped, paginated.TotalCount, paginated.CurrentPage, paginated.PageSize);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
