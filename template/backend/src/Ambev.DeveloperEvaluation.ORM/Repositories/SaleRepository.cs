using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="ISaleRepository"/> using Entity Framework Core.
/// </summary>
/// <remarks>
/// Provides data access logic for the Sale entity, enabling create, read, update, delete,
/// and query operations over the underlying PostgreSQL database.
/// </remarks>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the repository.
    /// </summary>
    /// <param name="sale">The sale to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale.</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier, including related items.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale if found, null otherwise.</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all sales from the repository, including related items.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A read-only collection of sales.</returns>
    public async Task<IReadOnlyCollection<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the repository.
    /// </summary>
    /// <param name="sale">The sale to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated sale.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the sale is not found.</exception>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existingSale = await _context.Sales
        .Include(s => s.Items)
        .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {sale.Id} not found.");

        // Atualiza os dados principais
        existingSale.IsCancelled = sale.IsCancelled;
        existingSale.Total = sale.Total;
        existingSale.TotalWithDiscount = sale.TotalWithDiscount;

        // Atualização inteligente dos itens
        var incomingItems = sale.Items;

        // Remove itens que não estão mais presentes
        var itemsToRemove = existingSale.Items
            .Where(existing => !incomingItems.Any(i => i.Id == existing.Id))
            .ToList();

        _context.SaleItems.RemoveRange(itemsToRemove);

        foreach (var incoming in incomingItems)
        {
            var existingItem = existingSale.Items.FirstOrDefault(e => e.Id == incoming.Id);

            if (existingItem != null)
            {
                // Atualiza os campos de itens existentes (exceto os calculados)
                existingItem.ProductId = incoming.ProductId;
                existingItem.ProductName = incoming.ProductName;
                existingItem.Quantity = incoming.Quantity;
                existingItem.UnitPrice = incoming.UnitPrice;
                existingItem.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                // Garante que CreatedAt esteja presente para novos itens
                incoming.CreatedAt = DateTime.UtcNow;

                // Adiciona novos itens (sem tentar setar campos calculados)
                existingSale.Items.Add(incoming);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return existingSale;
    }

    /// <summary>
    /// Cancels an existing sale by updating the IsCancelled flag and UpdatedAt timestamp.
    /// </summary>
    /// <param name="saleId">The ID of the sale to cancel.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated <see cref="Sale"/> instance.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the sale is not found.</exception>
    public async Task<Sale> CancelAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == saleId, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {saleId} not found.");

        sale.IsCancelled = true;
        sale.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }


    /// <summary>
    /// Deletes a sale from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Returns a queryable collection of sales with related items for read-only queries.
    /// </summary>
    /// <returns>A queryable collection of sales.</returns>
    public IQueryable<Sale> Query()
    {
        return _context.Sales
            .Include(s => s.Items)
            .AsNoTracking();
    }
}
