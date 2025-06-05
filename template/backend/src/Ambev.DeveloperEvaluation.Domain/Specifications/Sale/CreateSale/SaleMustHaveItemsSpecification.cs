namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sale.CreateSale;

/// <summary>
/// Specification that validates if a sale has at least one item.
/// </summary>
public class SaleMustHaveItemsSpecification : ISpecification<Entities.Sale>
{
    public bool IsSatisfiedBy(Entities.Sale sale)
    {
        return sale.Items != null && sale.Items.Any();
    }
}
