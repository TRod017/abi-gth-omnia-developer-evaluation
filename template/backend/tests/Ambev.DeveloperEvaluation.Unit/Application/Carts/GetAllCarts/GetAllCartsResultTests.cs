using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.GetAllCarts;

/// <summary>
/// Unit tests for the <see cref="GetAllCartsResult"/> class.
/// Verifies default initialization and property assignments.
/// </summary>
public class GetAllCartsResultTests
{
    /// <summary>
    /// Tests that the default constructor initializes properties correctly.
    /// </summary>
    [Fact(DisplayName = "Default GetAllCartsResult should have default values")]
    public void DefaultConstructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var result = new GetAllCartsResult();

        // Assert
        Assert.Equal(default(Guid), result.Id);
        Assert.Equal(default(Guid), result.UserId);
        Assert.Equal(default(CartStatus), result.Status);
        Assert.Equal(default(DateTime), result.CreatedAt);
        Assert.Null(result.UpdatedAt);
    }

    /// <summary>
    /// Tests that properties can be assigned and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Properties should be settable and gettable")]
    public void Properties_ShouldBeSetAndGetCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var status = CartStatus.Open;
        var createdAt = DateTime.UtcNow;
        var updatedAt = createdAt.AddHours(1);

        var result = new GetAllCartsResult
        {
            Id = id,
            UserId = userId,
            Status = status,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(status, result.Status);
        Assert.Equal(createdAt, result.CreatedAt);
        Assert.Equal(updatedAt, result.UpdatedAt);
    }
}
