using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart.CartItem;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="GetCartProfile"/> AutoMapper profile.
/// Validates that mapping configuration is valid and mappings between entities and DTOs work correctly.
/// Uses TestData for creating Cart and CartItem entities consistent with project standards.
/// </summary>
public class GetCartProfileTests
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes AutoMapper configuration and asserts it is valid.
    /// </summary>
    public GetCartProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetCartProfile>();
        });

        config.AssertConfigurationIsValid();

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Tests mapping from Cart entity to GetCartResult DTO.
    /// </summary>
    [Fact(DisplayName = "Should map Cart entity to GetCartResult DTO correctly")]
    public void Cart_To_GetCartResult_Mapping_IsValid()
    {
        var cart = CartTestData.GenerateValidCart();

        var result = _mapper.Map<GetCartResult>(cart);

        Assert.Equal(cart.Id, result.Id);
        Assert.Equal(cart.UserId, result.UserId);
        Assert.Equal(cart.Status.ToString(), result.Status);
        Assert.Equal(cart.CreatedAt, result.CreatedAt);
        Assert.Equal(cart.UpdatedAt, result.UpdatedAt);

        Assert.NotEmpty(result.Items);
        var firstItem = cart.Items.ElementAt(0);
        var firstResultItem = result.Items.ElementAt(0);

        Assert.Equal(firstItem.ProductId, firstResultItem.ProductId);
        Assert.Equal(firstItem.Quantity, firstResultItem.Quantity);
        Assert.Equal(firstItem.UnitPrice, firstResultItem.UnitPrice);
    }

    /// <summary>
    /// Tests mapping from CartItem entity to GetCartItemResult DTO.
    /// </summary>
    [Fact(DisplayName = "Should map CartItem entity to GetCartItemResult DTO correctly")]
    public void CartItem_To_GetCartItemResult_Mapping_IsValid()
    {
        var cartItem = CartTestData.GenerateValidCart().Items.First();

        var result = _mapper.Map<GetCartItemResult>(cartItem);

        Assert.Equal(cartItem.ProductId, result.ProductId);
        Assert.Equal(cartItem.Quantity, result.Quantity);
        Assert.Equal(cartItem.UnitPrice, result.UnitPrice);
        Assert.Equal(cartItem.Total, result.Total);
        Assert.Equal(cartItem.Discount, result.Discount);
        Assert.Equal(cartItem.TotalWithDiscount, result.TotalWithDiscount);
    }
}
