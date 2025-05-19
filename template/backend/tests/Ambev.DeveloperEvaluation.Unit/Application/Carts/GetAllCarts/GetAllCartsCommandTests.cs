using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.GetAllCarts;

/// <summary>
/// Unit tests for the <see cref="GetAllCartsCommand"/> class.
/// Validates default values and property setters.
/// </summary>
public class GetAllCartsCommandTests
{
    /// <summary>
    /// Tests that the default constructor sets Page and Size to expected defaults.
    /// </summary>
    [Fact(DisplayName = "Default constructor sets default values")]
    public void DefaultConstructor_ShouldSetDefaultValues()
    {
        // Arrange & Act
        var command = new GetAllCartsCommand();

        // Assert
        Assert.Equal(1, command.Page);
        Assert.Equal(10, command.Size);
    }

    /// <summary>
    /// Tests that properties Page and Size can be set and retrieved.
    /// </summary>
    [Fact(DisplayName = "Properties should be settable and gettable")]
    public void Properties_ShouldBeSetAndGetCorrectly()
    {
        // Arrange
        var command = new GetAllCartsCommand();

        // Act
        command.Page = 5;
        command.Size = 25;

        // Assert
        Assert.Equal(5, command.Page);
        Assert.Equal(25, command.Size);
    }
}
