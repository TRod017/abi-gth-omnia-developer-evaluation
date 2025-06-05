namespace Ambev.DeveloperEvaluation.Domain.Specifications.Cart;

/// <summary>
/// Specification that ensures a cart contains at least one item.
/// </summary>
public class CartMustHaveItemsSpecification : ISpecification<Entities.Cart>
{
    public bool IsSatisfiedBy(Entities.Cart cart)
    {
        return cart.Items != null && cart.Items.Any();
    }

    public string ErrorMessage => "The cart must contain at least one item.";
}
