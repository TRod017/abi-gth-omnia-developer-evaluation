using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler responsible for processing <see cref="CancelSaleCommand"/> requests.
/// </summary>
/// <remarks>
/// This handler retrieves the existing Sale from the repository,
/// applies the cancellation flag, validates business rules,
/// persists the change, and logs each step using <see cref="ILogger"/>.
/// </remarks>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CancelSaleHandler> _logger;

    // EventIds
    private static readonly EventId StartCancelEvent = new(3401, nameof(StartCancelEvent));
    private static readonly EventId SaleNotFoundEvent = new(3402, nameof(SaleNotFoundEvent));
    private static readonly EventId SaleCancelledEvent = new(3403, nameof(SaleCancelledEvent));
    private static readonly EventId UnexpectedErrorEvent = new(3499, nameof(UnexpectedErrorEvent));

    // LoggerMessage definitions
    private static readonly Action<ILogger, Guid, Exception?> LogStartCancel =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            StartCancelEvent,
            "Handling CancelSaleCommand for SaleId: {SaleId}");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleNotFound =
        LoggerMessage.Define<Guid>(
            LogLevel.Warning,
            SaleNotFoundEvent,
            "Sale with ID {SaleId} not found");

    private static readonly Action<ILogger, Guid, Exception?> LogSaleCancelled =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            SaleCancelledEvent,
            "Sale with ID {SaleId} cancelled successfully");

    private static readonly Action<ILogger, Guid, Exception> LogUnexpectedError =
        LoggerMessage.Define<Guid>(
            LogLevel.Error,
            UnexpectedErrorEvent,
            "Unexpected error while cancelling Sale with ID {SaleId}");

    public CancelSaleHandler(ISaleRepository repository, IMapper mapper, ILogger<CancelSaleHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            LogStartCancel(_logger, command.Id, null);

            var cancelledSale = await _repository.CancelAsync(command.Id, cancellationToken);

            LogSaleCancelled(_logger, cancelledSale.Id, null);

            return _mapper.Map<CancelSaleResult>(cancelledSale);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
