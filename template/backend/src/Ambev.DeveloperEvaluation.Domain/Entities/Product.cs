using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product available for sale.
/// </summary>
/// <remarks>
/// This entity stores basic product information such as name, description,
/// unit price, and the available stock quantity.
/// </remarks>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of a single unit of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the number of units currently available in stock.
    /// </summary>
    public int AvailableQuantity { get; set; }

    /// <summary>
    /// Validates the product using the <see cref="ProductValidator"/>.
    /// </summary>
    /// <returns>The validation result with any detected errors.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    /// <summary>
    /// Ensures that the requested quantity is available in stock.
    /// Throws <see cref="DomainException"/> if stock is insufficient.
    /// </summary>
    /// <param name="requestedQuantity">The quantity requested.</param>
    public void EnsureInStockOrThrow(int requestedQuantity)
    {
        if (AvailableQuantity < requestedQuantity)
        {
            throw new DomainException($"Insufficient stock for product '{Name}'. Requested: {requestedQuantity}, Available: {AvailableQuantity}");
        }
    }

    /// <summary>
    /// Ensures the product has a valid positive price.
    /// Throws <see cref="DomainException"/> if the price is invalid.
    /// </summary>
    public void EnsureValidPriceOrThrow()
    {
        if (UnitPrice <= 0)
        {
            throw new DomainException($"Product '{Name}' must have a positive price.");
        }
    }

    /// <summary>
    /// Ensures that the product respects all business rules.
    /// Throws <see cref="DomainException"/> if any rule is violated.
    /// </summary>
    public void EnsureBusinessRulesAreMet()
    {
        EnsureValidPriceOrThrow();
        // Add future domain validations here if needed.
    }
}
