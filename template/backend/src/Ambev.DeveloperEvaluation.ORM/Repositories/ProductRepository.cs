using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="IProductRepository"/> using Entity Framework Core.
/// </summary>
/// <remarks>
/// Provides data access logic for the Product entity, enabling create, read, update, delete,
/// and query operations over the underlying PostgreSQL database.
/// </remarks>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new product to the database asynchronously.
    /// </summary>
    /// <param name="product">The product entity to be added.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The created product entity after being saved.</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Retrieves a product entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The product entity if found; otherwise, null.</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all product entities asynchronously without tracking them in the context.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A read-only collection of all products.</returns>
    public async Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing product entity asynchronously in the database.
    /// </summary>
    /// <param name="product">The product entity containing updated data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated product entity after being saved.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the product to update is not found.</exception>
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

        if (existing == null)
            throw new KeyNotFoundException($"Product with ID {product.Id} not found.");

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.UnitPrice = product.UnitPrice;
        existing.AvailableQuantity = product.AvailableQuantity;

        _context.Products.Update(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    /// <summary>
    /// Deletes a product entity asynchronously by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><c>true</c> if the product was found and deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Provides a queryable collection of products configured for read-only operations.
    /// </summary>
    /// <remarks>
    /// This method returns an <see cref="IQueryable{Product}"/> with <c>AsNoTracking()</c>
    /// applied, allowing efficient filtering, ordering, and pagination without change tracking overhead.
    /// </remarks>
    /// <returns>An <see cref="IQueryable{Product}"/> for composing queries against the products.</returns>
    public IQueryable<Product> Query()
    {
        return _context.Products.AsNoTracking();
    }
}
