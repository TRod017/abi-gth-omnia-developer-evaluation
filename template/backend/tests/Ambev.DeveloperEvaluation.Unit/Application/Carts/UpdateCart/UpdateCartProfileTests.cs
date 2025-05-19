using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application.Profiles;

/// <summary>
/// Unit tests for the <see cref="UpdateCartProfile"/> AutoMapper profile.
/// Validates that mapping configurations are valid and mappings work as expected.
/// </summary>
public class UpdateCartProfileTests
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes AutoMapper configuration and asserts configuration validity.
    /// </summary>
    public UpdateCartProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UpdateCartProfile>();
        });

        config.AssertConfigurationIsValid();

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Tests mapping from UpdateCartCommand to Cart entity.
    /// </summary>
    [Fact(DisplayName = "Should map UpdateCartCommand to Cart entity correctly")]
    public void UpdateCartCommand_To_Cart_Mapping_IsValid()
    {
        var command = UpdateCartHandlerTestData.GenerateValidCommand();

        var cart = _mapper.Map<Cart>(command);

        cart.Id.Should().Be(command.Id);
        cart.UserId.Should().Be(command.UserId);
        cart.Status.Should().Be(command.Status);
        cart.Items.Should().HaveCount(command.Items.Count);

        var firstItem = cart.Items.First();
        var firstCommandItem = command.Items.First();

        firstItem.ProductId.Should().Be(firstCommandItem.ProductId);
        firstItem.Quantity.Should().Be(firstCommandItem.Quantity);
        firstItem.UnitPrice.Should().Be(firstCommandItem.UnitPrice);
    }

    /// <summary>
    /// Tests mapping from Cart entity to UpdateCartResult DTO.
    /// </summary>
    [Fact(DisplayName = "Should map Cart entity to UpdateCartResult DTO correctly")]
    public void Cart_To_UpdateCartResult_Mapping_IsValid()
    {
        var cart = CartTestData.GenerateValidCart();

        var result = _mapper.Map<UpdateCartResult>(cart);

        // Only Id is present in UpdateCartResult
        result.Id.Should().Be(cart.Id);
    }
}
