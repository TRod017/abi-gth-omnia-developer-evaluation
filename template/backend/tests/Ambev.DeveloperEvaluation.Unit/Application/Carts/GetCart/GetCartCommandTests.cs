using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.GetCart;

/// <summary>
/// Unit tests for the <see cref="GetCartCommand"/> class.
/// Validates constructors and property behavior.
/// </summary>
public class GetCartCommandTests
{
    /// <summary>
    /// Tests that the default constructor initializes properties with default values.
    /// </summary>
    [Fact(DisplayName = "Default constructor initializes properties with default values")]
    public void DefaultConstructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var command = new GetCartCommand();

        // Assert
        Assert.Equal(default(Guid), command.Id);
    }

    /// <summary>
    /// Tests that the constructor with an Id parameter sets the Id property correctly.
    /// </summary>
    [Fact(DisplayName = "Constructor with Id parameter sets the Id property correctly")]
    public void ParameterizedConstructor_ShouldSetIdProperty()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var command = new GetCartCommand(guid);

        // Assert
        Assert.Equal(guid, command.Id);
    }

    /// <summary>
    /// Tests that the Id property can be get and set correctly.
    /// </summary>
    [Fact(DisplayName = "Id property should be gettable and settable")]
    public void IdProperty_ShouldBeSettableAndGettable()
    {
        // Arrange
        var command = new GetCartCommand();
        var guid = Guid.NewGuid();

        // Act
        command.Id = guid;

        // Assert
        Assert.Equal(guid, command.Id);
    }
}
