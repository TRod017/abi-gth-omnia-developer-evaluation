using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common; // necessário para PaginatedList
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// Handler responsible for processing <see cref="GetAllCartsCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves all carts from the repository and maps them to the
/// <see cref="GetAllCartsResult"/> response model using <see cref="IMapper"/>. It also logs
/// the operation using <see cref="ILogger"/> for observability.
/// </remarks>
public class GetAllCartsHandler : IRequestHandler<GetAllCartsCommand, PaginatedList<GetAllCartsResult>>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCartsHandler> _logger;

    // EventIds
    private static readonly EventId RetrievingAllCartsEvent = new(3101, nameof(RetrievingAllCartsEvent));
    private static readonly EventId RetrievedCartsEvent = new(3102, nameof(RetrievedCartsEvent));
    private static readonly EventId GetAllCartsErrorEvent = new(3199, nameof(GetAllCartsErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Exception?> LogRetrievingAllCarts =
        LoggerMessage.Define(
            LogLevel.Information,
            RetrievingAllCartsEvent,
            "Retrieving all carts");

    private static readonly Action<ILogger, int, Exception?> LogRetrievedCarts =
        LoggerMessage.Define<int>(
            LogLevel.Information,
            RetrievedCartsEvent,
            "Retrieved {Count} carts");

    private static readonly Action<ILogger, Exception> LogUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            GetAllCartsErrorEvent,
            "Unexpected error while retrieving carts");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCartsHandler"/> class.
    /// </summary>
    /// <param name="repository">The cart repository for data access.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public GetAllCartsHandler(ICartRepository repository, IMapper mapper, ILogger<GetAllCartsHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetAllCartsCommand"/> request and returns a paginated list of carts.
    /// </summary>
    /// <param name="command">The request command with pagination parameters.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A paginated collection of <see cref="GetAllCartsResult"/>.</returns>
    public async Task<PaginatedList<GetAllCartsResult>> Handle(GetAllCartsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogRetrievingAllCarts(_logger, null);

            // Cria consulta paginada com EF usando o repositório
            var query = _repository.Query();
            var paginated = await PaginatedList<Cart>.CreateAsync(query, command.Page, command.Size, cancellationToken);

            LogRetrievedCarts(_logger, paginated.TotalCount, null);

            // Mapeia a lista de entidades Cart para GetAllCartsResult
            var mapped = _mapper.Map<List<GetAllCartsResult>>(paginated);

            // Retorna nova instância paginada com os resultados mapeados
            return new PaginatedList<GetAllCartsResult>(mapped, paginated.TotalCount, paginated.CurrentPage, paginated.PageSize);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
