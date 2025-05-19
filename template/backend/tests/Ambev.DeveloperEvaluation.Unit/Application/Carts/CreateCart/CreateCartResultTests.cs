using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CreateCart;

/// <summary>
/// Unit tests for the <see cref="CreateCartResult"/> class.
/// Verifies default initialization and property assignments.
/// </summary>
public class CreateCartResultTests
{
    /// <summary>
    /// Tests that the default constructor initializes properties correctly.
    /// </summary>
    [Fact(DisplayName = "Default CreateCartResult should have default values")]
    public void DefaultConstructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var result = new CreateCartResult();

        // Assert
        Assert.Equal(default(Guid), result.Id);
        Assert.Equal(default(decimal), result.Total);
        Assert.Equal(string.Empty, result.Status);
        Assert.Equal(default(DateTime), result.CreatedAt);
        Assert.NotNull(result.Items);
        Assert.Empty(result.Items);
    }

    /// <summary>
    /// Tests that properties can be assigned and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Properties should be settable and gettable")]
    public void Properties_ShouldBeSetAndGetCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var total = 123.45m;
        var status = "Open";
        var createdAt = DateTime.UtcNow;
        var items = new List<CreateCartItemResult>
        {
            new CreateCartItemResult { ProductId = Guid.NewGuid(), Quantity = 2, Total = 50m },
            new CreateCartItemResult { ProductId = Guid.NewGuid(), Quantity = 1, Total = 73.45m }
        };

        var result = new CreateCartResult
        {
            Id = id,
            Total = total,
            Status = status,
            CreatedAt = createdAt,
            Items = items
        };

        // Assert
        Assert.Equal(id, result.Id);
        Assert.Equal(total, result.Total);
        Assert.Equal(status, result.Status);
        Assert.Equal(createdAt, result.CreatedAt);
        Assert.Equal(items, result.Items);
    }
}
