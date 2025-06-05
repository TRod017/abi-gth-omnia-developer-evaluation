using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Product;

/// <summary>
/// Specification that ensures a product has a valid price (greater than zero).
/// </summary>
public class ProductMustHaveValidPriceSpecification : ISpecification<Entities.Product>
{
    public bool IsSatisfiedBy(Entities.Product product)
    {
        return product.UnitPrice > 0;
    }

    public string ErrorMessage => "The product price must be greater than zero.";
}
