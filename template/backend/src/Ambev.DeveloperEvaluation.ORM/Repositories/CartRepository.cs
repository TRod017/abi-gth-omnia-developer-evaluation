using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="ICartRepository"/> using Entity Framework Core.
/// </summary>
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

    /// <inheritdoc />
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    /// <inheritdoc />
    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
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

        // Atualiza os itens (remoção e adição)
        _context.CartItems.RemoveRange(existingCart.Items);
        await _context.CartItems.AddRangeAsync(cart.Items, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return existingCart;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public IQueryable<Cart> Query()
    {
        /// <summary>
        /// Returns an <see cref="IQueryable{Cart}"/> representing the base query for cart entities,
        /// including related cart items. This query is configured with <c>AsNoTracking</c> for
        /// optimized read-only scenarios such as filtering, sorting, and pagination.
        /// </summary>
        /// <returns>An <see cref="IQueryable{Cart}"/> with cart and item data.</returns>
        return _context.Carts
            .Include(c => c.Items)
            .AsNoTracking(); // Does not track entities in the context, improving read performance
    }
}
