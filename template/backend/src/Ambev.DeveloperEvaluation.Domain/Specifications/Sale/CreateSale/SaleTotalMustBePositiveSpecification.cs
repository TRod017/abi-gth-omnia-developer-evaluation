namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sale.CreateSale;

/// <summary>
/// Specification that ensures the sale total is greater than zero.
/// </summary>
public class SaleTotalMustBePositiveSpecification : ISpecification<Entities.Sale>
{
    public bool IsSatisfiedBy(Entities.Sale sale)
    {
        return sale.Total > 0;
    }
}
