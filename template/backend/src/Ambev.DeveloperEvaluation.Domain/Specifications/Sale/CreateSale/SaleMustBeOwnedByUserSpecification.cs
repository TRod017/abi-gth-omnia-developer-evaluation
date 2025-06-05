namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sale.CreateSale;

/// <summary>
/// Specification that ensures the sale belongs to the given user.
/// </summary>
public class SaleMustBeOwnedByUserSpecification : ISpecification<Entities.Sale>
{
    private readonly Guid _userId;

    public SaleMustBeOwnedByUserSpecification(Guid userId)
    {
        _userId = userId;
    }

    public bool IsSatisfiedBy(Entities.Sale sale)
    {
        return sale.UserId == _userId;
    }
}
