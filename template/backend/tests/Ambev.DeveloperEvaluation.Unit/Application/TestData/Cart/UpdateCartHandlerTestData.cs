using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;
using Bogus;
using Ambev.DeveloperEvaluation.Domain.Enums;


namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

/// <summary>
/// Provides test data for <see cref="UpdateCartCommand"/> and nested <see cref="UpdateCartItemCommand"/>.
/// </summary>
public static class UpdateCartHandlerTestData
{
    private static readonly Faker<UpdateCartItemCommand> itemFaker = new Faker<UpdateCartItemCommand>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 5))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 100));

    private static readonly Faker<UpdateCartCommand> faker = new Faker<UpdateCartCommand>()
        .RuleFor(c => c.Id, f => Guid.NewGuid())
        .RuleFor(c => c.UserId, f => Guid.NewGuid())
        .RuleFor(c => c.Status, f => f.PickRandom(CartStatus.Open, CartStatus.Confirmed)) // Somente os dois permitidos
        .RuleFor(c => c.Status, _ => CartStatus.Open)
        .RuleFor(c => c.Items, f => itemFaker.Generate(2));

    /// <summary>
    /// Generates a valid UpdateCartCommand with populated fields.
    /// </summary>
    public static UpdateCartCommand GenerateValidCommand()
    {
        var command = faker.Generate();

        // Garante que o Status não seja Unknown
        if (command.Status == CartStatus.Unknown)
            command.Status = CartStatus.Open;

        return command;
    }
}
