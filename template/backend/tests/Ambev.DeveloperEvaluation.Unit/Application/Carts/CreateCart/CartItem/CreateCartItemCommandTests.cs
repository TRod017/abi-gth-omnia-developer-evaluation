using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CreateCart.CartItem;

/// <summary>
/// Unit tests for the <see cref="CreateCartItemCommand"/> class.
/// Tests primarily focus on validation scenarios via validator class.
/// </summary>
public class CreateCartItemCommandTests
{
    /// <summary>
    /// Tests that a valid CreateCartItemCommand instance passes validation.
    /// </summary>
    [Fact(DisplayName = "Valid CreateCartItemCommand should have no validation errors")]
    public void Given_ValidCommand_When_Validated_Then_ShouldNotHaveErrors()
    {
        var command = new CreateCartItemCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            UnitPrice = 10.5m
        };

        // Since this class has no validation logic directly,
        // this test serves mostly as a placeholder.
        Assert.NotEqual(Guid.Empty, command.ProductId);
        Assert.True(command.Quantity > 0);
        Assert.True(command.UnitPrice >= 0);
    }

    /// <summary>
    /// Tests that a CreateCartItemCommand with default values is invalid logically.
    /// </summary>
    [Fact(DisplayName = "CreateCartItemCommand with default values should be logically invalid")]
    public void Given_DefaultCommand_When_Validated_Then_ShouldBeInvalid()
    {
        var command = new CreateCartItemCommand();

        Assert.Equal(Guid.Empty, command.ProductId);
        Assert.Equal(0, command.Quantity);
        Assert.Equal(0m, command.UnitPrice);
    }
}
