using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Handler responsible for processing <see cref="GetAllSalesCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves all Sales from the repository and maps them to the
/// <see cref="GetAllSalesResult"/> response model using <see cref="IMapper"/>. It also logs
/// the operation using <see cref="ILogger"/> for observability.
/// </remarks>
public class GetAllSalesHandler : IRequestHandler<GetAllSalesCommand, PaginatedList<GetAllSalesResult>>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;

    // EventIds
    private static readonly EventId RetrievingAllSalesEvent = new(3101, nameof(RetrievingAllSalesEvent));
    private static readonly EventId RetrievedSalesEvent = new(3102, nameof(RetrievedSalesEvent));
    private static readonly EventId GetAllSalesErrorEvent = new(3199, nameof(GetAllSalesErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Exception?> LogRetrievingAllSales =
        LoggerMessage.Define(
            LogLevel.Information,
            RetrievingAllSalesEvent,
            "Retrieving all Sales");

    private static readonly Action<ILogger, int, Exception?> LogRetrievedSales =
        LoggerMessage.Define<int>(
            LogLevel.Information,
            RetrievedSalesEvent,
            "Retrieved {Count} Sales");

    private static readonly Action<ILogger, Exception> LogUnexpectedError =
        LoggerMessage.Define(
            LogLevel.Error,
            GetAllSalesErrorEvent,
            "Unexpected error while retrieving Sales");

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesHandler"/> class.
    /// </summary>
    /// <param name="repository">The Sale repository for data access.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public GetAllSalesHandler(ISaleRepository repository, IMapper mapper, ILogger<GetAllSalesHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="GetAllSalesCommand"/> request and returns a paginated list of Sales.
    /// </summary>
    /// <param name="command">The request command with pagination parameters.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A paginated collection of <see cref="GetAllSalesResult"/>.</returns>
    public async Task<PaginatedList<GetAllSalesResult>> Handle(GetAllSalesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogRetrievingAllSales(_logger, null);

            // Cria consulta paginada com EF usando o repositório
            var query = _repository.Query();
            var paginated = await PaginatedList<Sale>.CreateAsync(query, command.Page, command.Size, cancellationToken);

            LogRetrievedSales(_logger, paginated.TotalCount, null);

            // Mapeia a lista de entidades Sale para GetAllSalesResult
            var mapped = _mapper.Map<List<GetAllSalesResult>>(paginated);

            // Retorna nova instância paginada com os resultados mapeados
            return new PaginatedList<GetAllSalesResult>(mapped, paginated.TotalCount, paginated.CurrentPage, paginated.PageSize);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, ex);
            throw;
        }
    }
}
