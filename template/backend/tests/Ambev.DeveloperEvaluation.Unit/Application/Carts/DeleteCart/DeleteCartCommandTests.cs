using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.DeleteCart;

/// <summary>
/// Unit tests for the <see cref="DeleteCartCommand"/> class.
/// Validates constructors, property initialization, and property getters/setters.
/// </summary>
public class DeleteCartCommandTests
{
    /// <summary>
    /// Tests that the default constructor initializes the Id property with the default Guid value.
    /// </summary>
    [Fact(DisplayName = "Default constructor initializes properties with default values")]
    public void DefaultConstructor_ShouldInitializeProperties()
    {
        var command = new DeleteCartCommand();

        Assert.Equal(default(Guid), command.Id);
    }

    /// <summary>
    /// Tests that the parameterized constructor sets the Id property correctly.
    /// </summary>
    [Fact(DisplayName = "Constructor with Id parameter sets the Id property correctly")]
    public void ParameterizedConstructor_ShouldSetIdProperty()
    {
        var guid = Guid.NewGuid();

        var command = new DeleteCartCommand(guid);

        Assert.Equal(guid, command.Id);
    }

    /// <summary>
    /// Tests that the Id property is gettable and settable.
    /// </summary>
    [Fact(DisplayName = "Id property should be gettable and settable")]
    public void IdProperty_ShouldBeSettableAndGettable()
    {
        var command = new DeleteCartCommand();
        var guid = Guid.NewGuid();

        command.Id = guid;

        Assert.Equal(guid, command.Id);
    }
}
