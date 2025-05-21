using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="ICartRepository"/> using Entity Framework Core.
/// </summary>
/// <remarks>
/// Provides data access logic for the Cart entity, enabling create, read, update, delete,
/// and query operations over the underlying PostgreSQL database.
/// </remarks>
public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public CartRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new cart entity asynchronously to the database.
    /// </summary>
    /// <param name="cart">The cart entity to be added.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The created cart entity after being saved.</returns>
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    /// <summary>
    /// Retrieves a cart entity by its unique identifier asynchronously,
    /// including related cart items.
    /// </summary>
    /// <param name="id">The unique identifier of the cart.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The cart entity with items if found; otherwise, null.</returns>
    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all cart entities asynchronously,
    /// including their related cart items.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A read-only collection of all carts with their items.</returns>
    public async Task<IReadOnlyCollection<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing cart entity asynchronously in the database,
    /// including its related cart items.
    /// </summary>
    /// <param name="cart">The cart entity containing updated data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated cart entity after being saved.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the cart to update is not found.</exception>
    public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        var existingCart = await _context.Carts
        .Include(c => c.Items)
        .FirstOrDefaultAsync(c => c.Id == cart.Id, cancellationToken);

        if (existingCart == null)
            throw new KeyNotFoundException($"Cart with ID {cart.Id} not found.");

        // Atualiza os dados principais
        existingCart.Status = cart.Status;
        existingCart.UserId = cart.UserId;

        // Identifica itens que foram removidos
        var itemsToRemove = existingCart.Items
            .Where(existingItem => !cart.Items.Any(newItem => newItem.Id == existingItem.Id))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            _context.CartItems.Remove(item);
        }

        // Atualiza ou adiciona os itens
        foreach (var item in cart.Items)
        {
            var existingItem = existingCart.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.ProductId = item.ProductId;
                existingItem.Quantity = item.Quantity;
                existingItem.UnitPrice = item.UnitPrice;
                existingItem.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                item.Id = Guid.NewGuid(); // Garante que o item tenha ID
                item.CartId = existingCart.Id;
                item.CreatedAt = DateTime.UtcNow;
                _context.CartItems.Add(item);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return existingCart;
    }

    /// <summary>
    /// Deletes a cart entity asynchronously by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><c>true</c> if the cart was found and deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Provides a queryable collection of cart entities including their items,
    /// configured for optimized read-only operations.
    /// </summary>
    /// <remarks>
    /// This method returns an <see cref="IQueryable{Cart}"/> with <c>AsNoTracking()</c> applied,
    /// allowing efficient filtering, sorting, and pagination without the overhead of change tracking.
    /// </remarks>
    /// <returns>An <see cref="IQueryable{Cart}"/> for composing queries against carts and their items.</returns>
    public IQueryable<Cart> Query()
    {
        return _context.Carts
            .Include(c => c.Items)
            .AsNoTracking(); // Does not track entities in the context, improving read performance
    }
}
