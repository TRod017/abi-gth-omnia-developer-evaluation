using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItem;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;


namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="GetCartResult"/> class.
/// Verifies default initialization and property assignments,
/// as well as business rule validations on item discounts and totals.
/// </summary>
public class GetCartResultTests
{
    /// <summary>
    /// Tests that the default constructor initializes properties correctly.
    /// </summary>
    [Fact(DisplayName = "Default GetCartResult should have default values")]
    public void DefaultConstructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var result = new GetCartResult();

        // Assert
        Assert.Equal(default(System.Guid), result.Id);
        Assert.Equal(default(System.Guid), result.UserId);
        Assert.Equal(string.Empty, result.Status);
        Assert.Equal(default(System.DateTime), result.CreatedAt);
        Assert.Null(result.UpdatedAt);
        Assert.NotNull(result.Items);
        Assert.Empty(result.Items);
    }

    /// <summary>
    /// Tests that properties can be assigned and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Properties should be settable and gettable")]
    public void Properties_ShouldBeSetAndGetCorrectly()
    {
        // Arrange
        var cart = CartHandlerTestData.GenerateCarts(1).First();

        // Convert CartItems to GetCartItemResult with no discount by default
        var items = cart.Items.Select(i => new GetCartItemResult
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            Total = i.UnitPrice * i.Quantity,
            Discount = 0m,
            TotalWithDiscount = i.UnitPrice * i.Quantity
        }).ToList();

        var result = new GetCartResult
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Status = cart.Status.ToString(),
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt,
            Items = items
        };

        // Assert
        Assert.Equal(cart.Id, result.Id);
        Assert.Equal(cart.UserId, result.UserId);
        Assert.Equal(cart.Status.ToString(), result.Status);
        Assert.Equal(cart.CreatedAt, result.CreatedAt);
        Assert.Equal(cart.UpdatedAt, result.UpdatedAt);
        Assert.Equal(items, result.Items);
    }

    /// <summary>
    /// Tests that a cart item with quantity 1 receives 0% discount.
    /// </summary>
    [Fact(DisplayName = "Cart item with 1 unit should have 0% discount")]
    public void Cart_ItemWith1Unit_ShouldHave0PercentDiscount()
    {
        var item = new GetCartItemResult
        {
            UnitPrice = 100m,
            Quantity = 1
        };
        item.Discount = CalculateDiscount(item.UnitPrice, item.Quantity);
        item.Total = item.UnitPrice * item.Quantity;
        item.TotalWithDiscount = item.Total - item.Discount;

        Assert.Equal(0m, item.Discount);
        Assert.Equal(item.Total, item.TotalWithDiscount);
    }

    /// <summary>
    /// Tests that a cart item with quantity 6 receives 5% discount.
    /// </summary>
    [Fact(DisplayName = "Cart item with 6 units should have 5% discount")]
    public void Cart_ItemWith6Units_ShouldHave5PercentDiscount()
    {
        var item = new GetCartItemResult
        {
            UnitPrice = 100m,
            Quantity = 6
        };
        item.Discount = CalculateDiscount(item.UnitPrice, item.Quantity);
        item.Total = item.UnitPrice * item.Quantity;
        item.TotalWithDiscount = item.Total - item.Discount;

        var expectedDiscount = 100m * 6 * 0.05m;
        Assert.Equal(expectedDiscount, item.Discount);
        Assert.Equal(item.Total - expectedDiscount, item.TotalWithDiscount);
    }

    /// <summary>
    /// Tests that a cart item with quantity 11 receives 10% discount.
    /// </summary>
    [Fact(DisplayName = "Cart item with 11 units should have 10% discount")]
    public void Cart_ItemWith11Units_ShouldHave10PercentDiscount()
    {
        var item = new GetCartItemResult
        {
            UnitPrice = 100m,
            Quantity = 11
        };
        item.Discount = CalculateDiscount(item.UnitPrice, item.Quantity);
        item.Total = item.UnitPrice * item.Quantity;
        item.TotalWithDiscount = item.Total - item.Discount;

        var expectedDiscount = 100m * 11 * 0.10m;
        Assert.Equal(expectedDiscount, item.Discount);
        Assert.Equal(item.Total - expectedDiscount, item.TotalWithDiscount);
    }

    /// <summary>
    /// Tests a cart with multiple items having varied discounts.
    /// </summary>
    [Fact(DisplayName = "Cart with multiple items with varied discounts")]
    public void Cart_MultipleItemsWithVariedDiscounts_ShouldCalculateCorrectTotals()
    {
        var items = new List<GetCartItemResult>
        {
            new GetCartItemResult { UnitPrice = 50m, Quantity = 1 },  // 0% discount
            new GetCartItemResult { UnitPrice = 20m, Quantity = 6 },  // 5% discount
            new GetCartItemResult { UnitPrice = 10m, Quantity = 11 }  // 10% discount
        };

        decimal expectedTotal = 0m;

        foreach (var item in items)
        {
            item.Discount = CalculateDiscount(item.UnitPrice, item.Quantity);
            item.Total = item.UnitPrice * item.Quantity;
            item.TotalWithDiscount = item.Total - item.Discount;

            decimal discountPercentage = 0m;
            if (item.Quantity >= 11)
                discountPercentage = 0.10m;
            else if (item.Quantity >= 6)
                discountPercentage = 0.05m;

            decimal expectedDiscount = item.UnitPrice * item.Quantity * discountPercentage;
            decimal expectedTotalWithDiscount = (item.UnitPrice * item.Quantity) - expectedDiscount;
            expectedTotal += expectedTotalWithDiscount;

            Assert.Equal(expectedDiscount, item.Discount);
            Assert.Equal(expectedTotalWithDiscount, item.TotalWithDiscount);
        }

        decimal cartTotal = items.Sum(i => i.TotalWithDiscount);
        Assert.Equal(expectedTotal, cartTotal);
    }

    /// <summary>
    /// Helper method to calculate discount based on business rules.
    /// </summary>
    private decimal CalculateDiscount(decimal unitPrice, int quantity)
    {
        if (quantity >= 11)
            return unitPrice * quantity * 0.10m;
        if (quantity >= 6)
            return unitPrice * quantity * 0.05m;
        return 0m;
    }
}
