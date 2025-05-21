using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
/// <summary>
/// Provides test data for DeleteCartHandler tests.
/// </summary>
public static class DeleteCartHandlerTestData
{
    /// <summary>
    /// Generates a valid DeleteCartCommand with a random Guid.
    /// </summary>
    public static DeleteCartCommand GenerateValidCommand()
    {
        return new DeleteCartCommand
        {
            Id = Guid.NewGuid()
        };
    }
}
