using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

/// <summary>
/// Unit tests for the <see cref="UpdateCartResult"/> class.
/// Verifies default initialization and property assignments.
/// </summary>
public class UpdateCartResultTests
{
    /// <summary>
    /// Tests that the default constructor initializes properties correctly.
    /// </summary>
    [Fact(DisplayName = "Default UpdateCartResult should have default values")]
    public void DefaultConstructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var result = new UpdateCartResult();

        // Assert
        Assert.Equal(default(Guid), result.Id);
    }

    /// <summary>
    /// Tests that properties can be assigned and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Properties should be settable and gettable")]
    public void Properties_ShouldBeSetAndGetCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();

        var result = new UpdateCartResult
        {
            Id = id
        };

        // Assert
        Assert.Equal(id, result.Id);
    }
}
