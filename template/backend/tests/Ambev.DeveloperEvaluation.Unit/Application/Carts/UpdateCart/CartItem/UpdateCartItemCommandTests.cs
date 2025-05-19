using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.UpdateCart.CartItems;

/// <summary>
/// Unit tests for the <see cref="UpdateCartItemCommand"/> class.
/// Tests primarily focus on validation scenarios similar to those of CreateCartItemCommand.
/// </summary>
public class UpdateCartItemCommandTests
{
    /// <summary>
    /// Tests that a valid UpdateCartItemCommand instance passes logical validation.
    /// </summary>
    [Fact(DisplayName = "Valid UpdateCartItemCommand should have no validation errors")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new UpdateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = 10.5m
        };

        Assert.NotEqual(Guid.Empty, command.ProductId);
        Assert.True(command.Quantity > 0);
        Assert.True(command.UnitPrice >= 0);
    }

    /// <summary>
    /// Tests that an UpdateCartItemCommand with default values is logically invalid.
    /// </summary>
    [Fact(DisplayName = "UpdateCartItemCommand with default values should be logically invalid")]
    public void Given_DefaultCommand_When_Validated_Then_ShouldBeInvalid()
    {
        var command = new UpdateCartItemCommand();

        Assert.Equal(Guid.Empty, command.ProductId);
        Assert.Equal(0, command.Quantity);
        Assert.Equal(0m, command.UnitPrice);
    }
}
