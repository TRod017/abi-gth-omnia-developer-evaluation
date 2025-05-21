using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="CreateCartProfile"/> AutoMapper profile.
/// Validates that mapping configurations are valid and mappings work as expected.
/// Uses TestData for entity creation to keep consistency with the project standard.
/// </summary>
public class CreateCartProfileTests
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes AutoMapper configuration and asserts configuration validity.
    /// </summary>
    public CreateCartProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateCartProfile>();
        });

        config.AssertConfigurationIsValid();

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Tests mapping from CreateCartCommand to Cart entity using TestData.
    /// </summary>
    [Fact(DisplayName = "Should map CreateCartCommand to Cart entity correctly")]
    public void CreateCartCommand_To_Cart_Mapping_IsValid()
    {
        var command = CreateCartHandlerTestData.GenerateValidCommand();

        var cart = _mapper.Map<Cart>(command);

        Assert.Equal(command.UserId, cart.UserId);
        Assert.Equal(command.Status, cart.Status);

        Assert.NotEmpty(cart.Items);
        var firstItem = cart.Items.ElementAt(0);
        var firstCommandItem = command.Items.ElementAt(0);

        Assert.Equal(firstCommandItem.ProductId, firstItem.ProductId);
        Assert.Equal(firstCommandItem.Quantity, firstItem.Quantity);
        Assert.Equal(firstCommandItem.UnitPrice, firstItem.UnitPrice);
    }

    /// <summary>
    /// Tests mapping from Cart entity to CreateCartResult DTO using TestData.
    /// </summary>
    [Fact(DisplayName = "Should map Cart entity to CreateCartResult DTO correctly")]
    public void Cart_To_CreateCartResult_Mapping_IsValid()
    {
        var cart = CartTestData.GenerateValidCart();

        var result = _mapper.Map<CreateCartResult>(cart);

        Assert.Equal(cart.Id, result.Id);
        Assert.Equal(cart.Status.ToString(), result.Status);
        Assert.Equal(cart.Total, result.Total);

        Assert.NotEmpty(result.Items);
        var firstItem = cart.Items.ElementAt(0);
        var firstResultItem = result.Items.ElementAt(0);

        Assert.Equal(firstItem.ProductId, firstResultItem.ProductId);
        Assert.Equal(firstItem.Quantity, firstResultItem.Quantity);
        Assert.Equal(firstItem.UnitPrice, firstResultItem.UnitPrice);
    }
}
