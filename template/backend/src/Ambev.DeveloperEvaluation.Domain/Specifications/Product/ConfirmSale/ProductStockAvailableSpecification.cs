namespace Ambev.DeveloperEvaluation.Domain.Specifications.Product.ConfirmSale;

using Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Specification that validates if there is enough stock for a requested product quantity.
/// </summary>
public class ProductStockAvailableSpecification : ISpecification<(Product product, int quantity)>
{
    /// <summary>
    /// Determines whether the specified product has sufficient stock for the requested quantity.
    /// </summary>
    /// <param name="data">A tuple containing the product and the requested quantity.</param>
    /// <returns><c>true</c> if available quantity is greater than or equal to requested quantity; otherwise, <c>false</c>.</returns>
    public bool IsSatisfiedBy((Product product, int quantity) data)
    {
        return data.product.AvailableQuantity >= data.quantity;
    }
}
