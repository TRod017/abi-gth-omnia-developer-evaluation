using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications.Sale.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Specifications.Sale.CreateSale; // Specification: importação

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

            var sale = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (sale == null)
            {
                LogSaleNotFound(_logger, command.Id, null);
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found.");
            }

            // Specification: Ensure sale has not been cancelled before
            var alreadyCancelledSpec = new SaleCannotBeCancelledTwiceSpecification();
            if (!alreadyCancelledSpec.IsSatisfiedBy(sale))
                throw new InvalidOperationException("Sale has already been cancelled.");

            // Specification (optional): Ensure sale belongs to authenticated user (for multi-user scenarios)
            var ownershipSpec = new SaleMustBeOwnedByUserSpecification(command.UserId);
            if (!ownershipSpec.IsSatisfiedBy(sale))
                throw new InvalidOperationException("User does not own this sale.");

            sale.IsCancelled = true;
            sale.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(sale, cancellationToken);

            LogSaleCancelled(_logger, sale.Id, null);

            return _mapper.Map<CancelSaleResult>(sale);
        }
        catch (Exception ex)
        {
            LogUnexpectedError(_logger, command.Id, ex);
            throw;
        }
    }
}
