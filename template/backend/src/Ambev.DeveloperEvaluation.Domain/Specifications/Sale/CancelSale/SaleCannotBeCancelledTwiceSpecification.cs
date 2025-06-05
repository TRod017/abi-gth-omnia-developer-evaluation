namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sale.CancelSale;


/// <summary>
/// Specification that prevents cancelling an already cancelled sale.
/// </summary>
public class SaleCannotBeCancelledTwiceSpecification : ISpecification<Entities.Sale>
{
    public bool IsSatisfiedBy(Entities.Sale sale)
    {
        return !sale.IsCancelled;
    }
}
