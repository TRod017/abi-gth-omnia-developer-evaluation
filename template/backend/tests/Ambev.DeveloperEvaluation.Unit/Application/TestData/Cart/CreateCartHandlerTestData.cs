using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

/// <summary>
/// Provides test data generation methods for CreateCartCommand and CreateCartItemCommand
/// using the Bogus library. 
/// This class centralizes the creation of valid commands with realistic sample data
/// to be used in unit tests for the CreateCartHandler.
/// </summary>
public static class CreateCartHandlerTestData
{
    private static readonly Faker<CreateCartItemCommand> itemFaker = new Faker<CreateCartItemCommand>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 5));

    private static readonly Faker<CreateCartCommand> faker = new Faker<CreateCartCommand>()
        .RuleFor(c => c.UserId, f => Guid.NewGuid())
        .RuleFor(c => c.Items, f => itemFaker.Generate(3));

    /// <summary>
    /// Generates a valid CreateCartCommand with populated fields and multiple items.
    /// </summary>
    public static CreateCartCommand GenerateValidCommand() => faker.Generate();
}
